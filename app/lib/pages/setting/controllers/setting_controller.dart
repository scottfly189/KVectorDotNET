import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:projectzoom/controllers/app_controller.dart';
import 'package:projectzoom/utils/language_select.dart';

class SettingController extends GetxController {
  final RxString userName = ''.obs;

  @override
  void onInit() {
    super.onInit();
    userName.value = 'M1120234';
  }

  showLanguageDialog(BuildContext context) {
    LanguageSelect.showLanguageDialog(context, () {
      update();
      AppController.to.updateLanguage();
    });
  }
}
