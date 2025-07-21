// DO NOT EDIT. This is code generated via package:intl/generate_localized.dart
// This is a library that provides messages for a ja locale. All the
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
  String get localeName => 'ja';

  static String m0(name) => "こんにちは、${name}";

  static String m1(name) => "ユーザー名:${name}";

  final messages = _notInlinedMessages(_notInlinedMessages);
  static Map<String, Function> _notInlinedMessages(_) => <String, Function>{
    "about": MessageLookupByLibrary.simpleMessage("について"),
    "account": MessageLookupByLibrary.simpleMessage("アカウント"),
    "account_info": MessageLookupByLibrary.simpleMessage("アカウント情報"),
    "app_name": MessageLookupByLibrary.simpleMessage("ProjectZoom建築施工管理システム"),
    "cancel": MessageLookupByLibrary.simpleMessage("キャンセル"),
    "change_language": MessageLookupByLibrary.simpleMessage("言語を変更"),
    "confirm": MessageLookupByLibrary.simpleMessage("確認"),
    "daily_log": MessageLookupByLibrary.simpleMessage("日志"),
    "forget_password": MessageLookupByLibrary.simpleMessage("パスワードを忘れた?"),
    "help_and_feedback": MessageLookupByLibrary.simpleMessage("ヘルプとフィードバック"),
    "hi": m0,
    "job": MessageLookupByLibrary.simpleMessage("タスク"),
    "language": MessageLookupByLibrary.simpleMessage("言語を変更"),
    "login_fail": MessageLookupByLibrary.simpleMessage("ログインに失敗しました。再試行してください"),
    "logout": MessageLookupByLibrary.simpleMessage("ログアウト"),
    "logout_confirm": MessageLookupByLibrary.simpleMessage("本当にログアウトしますか?"),
    "my": MessageLookupByLibrary.simpleMessage("プロフィール"),
    "password": MessageLookupByLibrary.simpleMessage("パスワード"),
    "print_setting": MessageLookupByLibrary.simpleMessage("印刷設定"),
    "project": MessageLookupByLibrary.simpleMessage("プロジェクト"),
    "register": MessageLookupByLibrary.simpleMessage("登録"),
    "register_now": MessageLookupByLibrary.simpleMessage("今すぐ登録"),
    "report": MessageLookupByLibrary.simpleMessage("レポート"),
    "schedule": MessageLookupByLibrary.simpleMessage("計画"),
    "search": MessageLookupByLibrary.simpleMessage("検索..."),
    "select_project": MessageLookupByLibrary.simpleMessage("プロジェクトを選択"),
    "setting": MessageLookupByLibrary.simpleMessage("プロフィール"),
    "sign_in": MessageLookupByLibrary.simpleMessage("ログイン"),
    "sub_title": MessageLookupByLibrary.simpleMessage("建築、設備管理"),
    "system_setting": MessageLookupByLibrary.simpleMessage("システム設定"),
    "talk_hub": MessageLookupByLibrary.simpleMessage("Talk Hub"),
    "tickets": MessageLookupByLibrary.simpleMessage("不具合"),
    "user_info": m1,
    "version": MessageLookupByLibrary.simpleMessage("バージョン"),
  };
}
