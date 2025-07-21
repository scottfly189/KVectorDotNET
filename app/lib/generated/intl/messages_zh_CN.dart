// DO NOT EDIT. This is code generated via package:intl/generate_localized.dart
// This is a library that provides messages for a zh_CN locale. All the
// messages from the main program should be duplicated here with the same
// function name.

// Ignore issues from commonly used lints in this file.
// ignore_for_file:unnecessary_brace_in_string_interps, unnecessary_new
// ignore_for_file:prefer_single_quotes,comment_references, directives_ordering
// ignore_for_file:annotate_overrides,prefer_generic_function_type_aliases
// ignore_for_file:unused_import, file_names, avoid_escaping_inner_quotes
// ignore_for_file:unnecessary_string_interpolations, unnecessary_string_escapes

import 'package:intl/intl.dart';
import 'package:intl/message_lookup_by_library.dart';

final messages = new MessageLookup();

typedef String MessageIfAbsent(String messageStr, List<dynamic> args);

class MessageLookup extends MessageLookupByLibrary {
  String get localeName => 'zh_CN';

  static String m0(name) => "嗨，${name}";

  static String m1(name) => "用户名:${name}";

  final messages = _notInlinedMessages(_notInlinedMessages);
  static Map<String, Function> _notInlinedMessages(_) => <String, Function>{
    "about": MessageLookupByLibrary.simpleMessage("关于"),
    "account": MessageLookupByLibrary.simpleMessage("账户"),
    "account_info": MessageLookupByLibrary.simpleMessage("账户信息"),
    "app_name": MessageLookupByLibrary.simpleMessage("ProjectZoom建筑施工管理系统"),
    "cancel": MessageLookupByLibrary.simpleMessage("取消"),
    "change_language": MessageLookupByLibrary.simpleMessage("更改语言"),
    "confirm": MessageLookupByLibrary.simpleMessage("确认"),
    "daily_log": MessageLookupByLibrary.simpleMessage("现场日志"),
    "forget_password": MessageLookupByLibrary.simpleMessage("忘记密码?"),
    "help_and_feedback": MessageLookupByLibrary.simpleMessage("帮助与反馈"),
    "hi": m0,
    "job": MessageLookupByLibrary.simpleMessage("任务管理"),
    "language": MessageLookupByLibrary.simpleMessage("更改语言"),
    "login_fail": MessageLookupByLibrary.simpleMessage("登录失败，请重试"),
    "logout": MessageLookupByLibrary.simpleMessage("退出登录"),
    "logout_confirm": MessageLookupByLibrary.simpleMessage("确定要登出吗?"),
    "my": MessageLookupByLibrary.simpleMessage("我的"),
    "password": MessageLookupByLibrary.simpleMessage("密码"),
    "print_setting": MessageLookupByLibrary.simpleMessage("打印设置"),
    "project": MessageLookupByLibrary.simpleMessage("项目"),
    "register": MessageLookupByLibrary.simpleMessage("注册"),
    "register_now": MessageLookupByLibrary.simpleMessage("立即注册"),
    "report": MessageLookupByLibrary.simpleMessage("报表"),
    "schedule": MessageLookupByLibrary.simpleMessage("计划"),
    "search": MessageLookupByLibrary.simpleMessage("搜索..."),
    "select_project": MessageLookupByLibrary.simpleMessage("选择项目"),
    "setting": MessageLookupByLibrary.simpleMessage("我的"),
    "sign_in": MessageLookupByLibrary.simpleMessage("登 录"),
    "sub_title": MessageLookupByLibrary.simpleMessage("建筑、设施管理"),
    "system_setting": MessageLookupByLibrary.simpleMessage("系统设置"),
    "talk_hub": MessageLookupByLibrary.simpleMessage("沟通中心"),
    "tickets": MessageLookupByLibrary.simpleMessage("缺陷工单"),
    "user_info": m1,
    "version": MessageLookupByLibrary.simpleMessage("版本"),
  };
}
