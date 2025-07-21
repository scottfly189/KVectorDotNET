import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:projectzoom/generated/l10n.dart';
import 'package:projectzoom/services/storeage.dart';
import 'package:projectzoom/utils/language_list.dart';
import 'dart:convert';

class LanguageService extends GetxService {
  static LanguageService get to => Get.find();

  String defaultLanguage = "en";
  LanguageItem? currentLanguage =
      LanguageList.languageList.firstWhere((item) => item.code == "en");

  /// 初始化语言服务
  Future<LanguageService> init() async {
    return this;
  }

  /// 获取当前语言
  Locale getLanguage() {
    final languageObject = StorageService.to.getString("language");
    if (languageObject.isNotEmpty) {
      final language = jsonDecode(languageObject);
      final languageItem = LanguageList.languageList.firstWhere((element) =>
          element.code == language["code"] &&
          element.countryCode == language["countryCode"]);
      currentLanguage = languageItem;
      Locale locale = languageItem.countryCode.isEmpty
          ? Locale(languageItem.code)
          : Locale(languageItem.code, languageItem.countryCode);

      return locale;
    }

    return Locale(defaultLanguage);
  }

  /// 设置语言
  Future<void> setLanguage(LanguageItem language) async {
    currentLanguage = language;
    await StorageService.to
        .setString("language", jsonEncode(language.toJson()));
    await S.load(language.countryCode.isEmpty
        ? Locale(language.code)
        : Locale(language.code, language.countryCode));
  }
}
