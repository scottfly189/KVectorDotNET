import 'package:get/get.dart';

class UserConfig extends GetxService {
  static UserConfig get to => Get.find();

  bool isLogin = false;

  Future<UserConfig> init() async {
    return this;
  }
}
