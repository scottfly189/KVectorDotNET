import 'package:get/get.dart';
import 'package:shared_preferences/shared_preferences.dart';

/// 存储服务
class StorageService extends GetxService {
  static StorageService get to => Get.find();
  late final SharedPreferences _prefs;

  Future<StorageService> init() async {
    _prefs = await SharedPreferences.getInstance();
    return this;
  }

  Future<bool> setString(String key, String value) async {
    return await _prefs.setString(key, value);
  }

  Future<bool> setBool(String key, bool value) async {
    return await _prefs.setBool(key, value);
  }

  Future<bool> setList(String key, List<String> value) async {
    return await _prefs.setStringList(key, value);
  }

  Future<bool> setInt(String key, int value) async {
    return await _prefs.setInt(key, value);
  }

  Future<bool> setDouble(String key, double value) async {
    return await _prefs.setDouble(key, value);
  }

  String getString(String key) {
    return _prefs.getString(key) ?? '';
  }

  bool getBool(String key) {
    return _prefs.getBool(key) ?? false;
  }

  List<String>? getList(String key) {
    return _prefs.getStringList(key);
  }

  int? getInt(String key) {
    return _prefs.getInt(key);
  }

  double? getDouble(String key) {
    return _prefs.getDouble(key);
  }

  Future<bool> remove(String key) async {
    return await _prefs.remove(key);
  }

  Future<bool> clear() async {
    return await _prefs.clear();
  }
}
