import 'package:get/get.dart';
import 'package:projectzoom/controllers/app_controller.dart';
import 'package:projectzoom/services/language_service.dart';
import 'package:projectzoom/services/storeage.dart';
import 'package:projectzoom/services/user_config.dart';

class Global {
  static const String appName = 'Project Zoom';

  static Future<void> init() async {
    await Future.wait([
      Get.putAsync(() => StorageService().init()),
      Get.putAsync(() => UserConfig().init()),
    ]);
    await Get.putAsync(() => LanguageService().init());
    await Get.putAsync(() => AppController().init(), permanent: true);
  }
}
