import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:get/get.dart';
import 'package:projectzoom/generated/l10n.dart';
import 'package:projectzoom/global.dart';
import 'package:projectzoom/routes/routes.dart';
import 'package:projectzoom/services/language_service.dart';
import 'package:projectzoom/utils/http_utils.dart';
import 'package:flutter_easyloading/flutter_easyloading.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await Global.init();
  HttpUtils.init();
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return GetMaterialApp(
      debugShowCheckedModeBanner: false,
      home: MaterialApp.router(
        title: 'Project Zoom',
        routerConfig: Routes.router,
        debugShowCheckedModeBanner: false,
        localizationsDelegates: const [
          GlobalMaterialLocalizations.delegate,
          GlobalWidgetsLocalizations.delegate,
          GlobalCupertinoLocalizations.delegate,
          S.delegate,
        ],
        supportedLocales: S.delegate.supportedLocales,
        locale: LanguageService.to.getLanguage(),
        builder: EasyLoading.init(),
      ),
    );
  }
}
