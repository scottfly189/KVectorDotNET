class NetConfig {
  static const String runMode = 'test'; //两个值: test 和 prod

  static String get apiUrl {
    if (runMode == 'test') {
      return _apiUrlTest;
    }
    return _apiUrl;
  }

  static String get prodHost {
    return "api.projectzoom.com.au";
  }

  /*
  * 正式环境
  */
  static const String _apiUrl = 'https://api.projectzoom.com.au';
  /*
  * 测试环境
  */
  static const String _apiUrlTest = 'http://192.168.1.7:5005';
}
