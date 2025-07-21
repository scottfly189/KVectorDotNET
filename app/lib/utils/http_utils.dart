import 'dart:convert';
import 'dart:io';
import 'package:dio/dio.dart';
import 'package:dio/io.dart';
import 'package:projectzoom/services/storeage.dart';
import 'package:projectzoom/config/netconfig.dart';

class HttpUtils {
  // ignore: constant_identifier_names
  static const String ACCESS_TOKEN_KEY = 'access-token';
  // ignore: constant_identifier_names
  static const String REFRESH_TOKEN_KEY = 'x-access-token';

  static late final Dio _dio;
  static final StorageService _prefs = StorageService.to;

  // 初始化 Dio
  static void init() {
    _dio = Dio(BaseOptions(
      baseUrl: NetConfig.apiUrl,
      connectTimeout: const Duration(seconds: 30),
      receiveTimeout: const Duration(seconds: 30),
      sendTimeout: const Duration(seconds: 30),
      validateStatus: (status) {
        return status != null && status < 500;
      },
    ));

    // 配置证书验证
    (_dio.httpClientAdapter as IOHttpClientAdapter).createHttpClient = () {
      final client = HttpClient();
      client.badCertificateCallback =
          (X509Certificate cert, String host, int port) {
        if (NetConfig.runMode == 'prod') {
          return host == NetConfig.prodHost;
        }
        return true;
      };
      return client;
    };

    // 添加拦截器
    _dio.interceptors.add(InterceptorsWrapper(
      onRequest: _requestInterceptor,
      onResponse: _responseInterceptor,
      onError: _errorInterceptor,
    ));
  }

  // 请求拦截器
  static void _requestInterceptor(
      RequestOptions options, RequestInterceptorHandler handler) async {
    final String accessToken = _prefs.getString(ACCESS_TOKEN_KEY);

    if (accessToken != "") {
      options.headers['Authorization'] = 'Bearer $accessToken';

      // 检查token是否过期
      final jwt = _decryptJWT(accessToken);
      final exp = _getJWTDate(jwt['exp']);

      if (DateTime.now().isAfter(exp)) {
        final refreshToken = _prefs.getString(REFRESH_TOKEN_KEY);
        if (refreshToken != "") {
          options.headers['X-Authorization'] = 'Bearer $refreshToken';
        }
      }
    }

    // 添加语言设置
    final String language = _prefs.getString('globalI18n');
    if (language != "") {
      options.headers['Accept-Language'] = language;
    }

    handler.next(options);
  }

  // 响应拦截器
  static void _responseInterceptor(
      Response response, ResponseInterceptorHandler handler) async {
    final headers = response.headers;
    final accessToken = headers[ACCESS_TOKEN_KEY]?.first;
    final refreshToken = headers[REFRESH_TOKEN_KEY]?.first;

    if (accessToken == 'invalid_token') {
      await clearAccessTokens();
    } else if (refreshToken != null &&
        accessToken != null &&
        accessToken != 'invalid_token') {
      await _prefs.setString(ACCESS_TOKEN_KEY, accessToken);
      await _prefs.setString(REFRESH_TOKEN_KEY, refreshToken);
    }

    // 处理响应状态
    if (response.statusCode == 401) {
      await clearAccessTokens();
    }

    final data = response.data;
    if (data is Map && data.containsKey('code')) {
      if (data['code'] == 401) {
        await clearAccessTokens();
      } else if (data['code'] != 200) {
        String message = data['message'] is Map
            ? json.encode(data['message'])
            : data['message'].toString();
        handler.reject(
            DioException(
              requestOptions: response.requestOptions,
              message: message,
            ),
            true);
        return;
      }
    }

    handler.next(response);
  }

  // 错误拦截器
  static void _errorInterceptor(
      DioException error, ErrorInterceptorHandler handler) async {
    if (error.response?.statusCode == 401) {
      await clearAccessTokens();
    }
    handler.next(error);
  }

  // 清除令牌
  static Future<void> clearAccessTokens() async {
    await _prefs.remove(ACCESS_TOKEN_KEY);
    await _prefs.remove(REFRESH_TOKEN_KEY);
    await _prefs.remove('globalI18n');
  }

  // JWT解密
  static Map<String, dynamic> _decryptJWT(String token) {
    final parts = token.split('.');
    if (parts.length != 3) {
      throw Exception('Invalid token');
    }

    final payload = parts[1].replaceAll('_', '/').replaceAll('-', '+');
    final normalized = base64Url.normalize(payload);
    final resp = utf8.decode(base64Url.decode(normalized));
    return json.decode(resp);
  }

  // JWT时间转换
  static DateTime _getJWTDate(int timestamp) {
    return DateTime.fromMillisecondsSinceEpoch(timestamp * 1000);
  }

  // 包装请求方法
  static Future<T?> request<T>({
    required String path,
    String method = 'GET',
    Map<String, dynamic>? queryParameters,
    dynamic data,
    Options? options,
  }) async {
    try {
      final response = await _dio.request(
        path,
        data: data,
        queryParameters: queryParameters,
        options: options ?? Options(method: method),
      );
      return response.data;
    } on DioException catch (e) {
      if (e.type == DioExceptionType.connectionTimeout ||
          e.type == DioExceptionType.connectionError) {
        throw DioException(
          requestOptions: e.requestOptions,
          message: '无法连接到服务器，请检查网络连接或服务器状态',
        );
      }
      rethrow;
    }
  }
}
