import 'package:country_flags/country_flags.dart';
import 'package:flutter/material.dart';
import 'package:projectzoom/services/language_service.dart';
import 'package:projectzoom/utils/language_list.dart';

class LanguageSelect {
  static void showLanguageDialog(BuildContext context, Function func) {
    showModalBottomSheet(
      context: context,
      builder: (BuildContext context) {
        return Container(
          padding: EdgeInsets.all(16),
          child: ListView(
            shrinkWrap: true,
            children: [
              LayoutBuilder(
                builder: (context, constraints) {
                  // 计算最长的文本宽度
                  double maxWidth =
                      LanguageList.languageList.fold(0.0, (max, lang) {
                    final textWidth = TextPainter(
                      text: TextSpan(
                        text: lang.name,
                        style: TextStyle(fontSize: 16),
                      ),
                      maxLines: 1,
                      textDirection: TextDirection.ltr,
                    )..layout();
                    return textWidth.width > max ? textWidth.width : max;
                  });

                  // 添加图标宽度和间距
                  maxWidth += 100; // 图标宽度(16) + 间距(4) + 一些额外空间(4)

                  return Column(
                    children: LanguageList.languageList.map((e) {
                      return ListTile(
                        title: Center(
                          child: SizedBox(
                            width: maxWidth,
                            child: Row(
                              mainAxisSize: MainAxisSize.max,
                              children: [
                                CountryFlag.fromLanguageCode(
                                  e.isoLanguageCode,
                                  width: 50,
                                  height: 30,
                                ),
                                SizedBox(width: 8),
                                Text(
                                  e.name,
                                  style: TextStyle(
                                    fontSize: 16,
                                    fontWeight: LanguageService
                                                    .to.currentLanguage?.code ==
                                                e.code &&
                                            LanguageService.to.currentLanguage
                                                    ?.countryCode ==
                                                e.countryCode
                                        ? FontWeight.bold
                                        : FontWeight.normal,
                                    color: LanguageService
                                                    .to.currentLanguage?.code ==
                                                e.code &&
                                            LanguageService.to.currentLanguage
                                                    ?.countryCode ==
                                                e.countryCode
                                        ? Colors.grey[400]
                                        : Colors.black,
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                        contentPadding: EdgeInsets.symmetric(horizontal: 16),
                        onTap: () async {
                          await LanguageService.to.setLanguage(e);
                          if (context.mounted) {
                            Navigator.pop(context);
                            func();
                          }
                        },
                      );
                    }).toList(),
                  );
                },
              ),
            ],
          ),
        );
      },
    );
  }
}
