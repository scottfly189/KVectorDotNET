import 'package:get/get.dart';

class AppController extends GetxController {
  static AppController get to => Get.find();

  // 未读消息数量
  final RxInt unreadMessageCount = 0.obs;

  // 语言版本
  final RxInt languageVersion = 0.obs;

  /// 初始化
  Future<AppController> init() async {
    unreadMessageCount.value = 10;
    return this;
  }

  // 更新语言版本
  void updateLanguage() {
    languageVersion.value++;
    update();
  }
}
