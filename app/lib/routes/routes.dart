import 'package:go_router/go_router.dart';
import 'package:projectzoom/pages/home/home_page.dart';
import 'package:projectzoom/pages/job/job_page.dart';
import 'package:projectzoom/pages/setting/setting_page.dart';
import 'package:projectzoom/pages/sign/sign_page.dart';

import 'package:projectzoom/pages/ticket/ticket_page.dart';
import 'package:projectzoom/services/user_config.dart';

/*
  路由表
*/
class Routes {
  static const String job = 'job'; //接件和派件
  static const String tickets = 'tickets'; //中转
  static const String setting = 'setting'; //设置

  static const String login = 'login'; //登录

  static final GoRouter router = GoRouter(
    initialLocation: '/',
    routes: <GoRoute>[
      GoRoute(
        path: '/',
        builder: (context, state) => const HomePageFramework(),
        redirect: RouteRedirect.auth,
      ),
      GoRoute(
        path: '/$job',
        name: 'job',
        builder: (context, state) => const JobPage(),
        redirect: RouteRedirect.auth,
      ),
      GoRoute(
        path: '/$tickets',
        name: 'tickets',
        builder: (context, state) => const TicketsPage(),
        redirect: RouteRedirect.auth,
      ),
      GoRoute(
          path: '/$setting',
          name: 'setting',
          builder: (context, state) => const SettingPage(),
          redirect: RouteRedirect.auth),
      GoRoute(
          path: '/$login',
          name: 'login',
          builder: (context, state) => const SignPage()),
    ],
  );
}

abstract class RouteRedirect {
  static String? auth(context, state) {
    if (UserConfig.to.isLogin) {
      return null;
    }
    return '/${Routes.login}';
  }
}
