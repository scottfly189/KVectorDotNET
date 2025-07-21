// DO NOT EDIT. This is code generated via package:intl/generate_localized.dart
// This is a library that provides messages for a vi locale. All the
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
  String get localeName => 'vi';

  static String m0(name) => "Xin chào, ${name}";

  static String m1(name) => "Tên người dùng: ${name}";

  final messages = _notInlinedMessages(_notInlinedMessages);
  static Map<String, Function> _notInlinedMessages(_) => <String, Function>{
    "about": MessageLookupByLibrary.simpleMessage("Giới thiệu"),
    "account": MessageLookupByLibrary.simpleMessage("Tài khoản"),
    "account_info": MessageLookupByLibrary.simpleMessage("Thông tin tài khoản"),
    "app_name": MessageLookupByLibrary.simpleMessage(
      "ProjectZoom Quản lý xây dựng và cơ sở vật chất",
    ),
    "cancel": MessageLookupByLibrary.simpleMessage("Hủy"),
    "change_language": MessageLookupByLibrary.simpleMessage(
      "Thay đổi ngôn ngữ",
    ),
    "confirm": MessageLookupByLibrary.simpleMessage("Xác nhận"),
    "daily_log": MessageLookupByLibrary.simpleMessage("Nhật ký"),
    "forget_password": MessageLookupByLibrary.simpleMessage("Quên mật khẩu?"),
    "help_and_feedback": MessageLookupByLibrary.simpleMessage(
      "Trợ giúp và phản hồi",
    ),
    "hi": m0,
    "job": MessageLookupByLibrary.simpleMessage("Công việc"),
    "language": MessageLookupByLibrary.simpleMessage("Thay đổi ngôn ngữ"),
    "login_fail": MessageLookupByLibrary.simpleMessage(
      "Đăng nhập thất bại, vui lòng thử lại",
    ),
    "logout": MessageLookupByLibrary.simpleMessage("Đăng xuất"),
    "logout_confirm": MessageLookupByLibrary.simpleMessage(
      "Bạn có chắc chắn muốn đăng xuất?",
    ),
    "my": MessageLookupByLibrary.simpleMessage("Hồ sơ"),
    "password": MessageLookupByLibrary.simpleMessage("Mật khẩu"),
    "print_setting": MessageLookupByLibrary.simpleMessage("Cài đặt in"),
    "project": MessageLookupByLibrary.simpleMessage("Dự án"),
    "register": MessageLookupByLibrary.simpleMessage("Đăng ký"),
    "register_now": MessageLookupByLibrary.simpleMessage("Đăng ký ngay"),
    "report": MessageLookupByLibrary.simpleMessage("Báo cáo"),
    "schedule": MessageLookupByLibrary.simpleMessage("Lịch trình"),
    "search": MessageLookupByLibrary.simpleMessage("Tìm kiếm..."),
    "select_project": MessageLookupByLibrary.simpleMessage("Chọn dự án"),
    "setting": MessageLookupByLibrary.simpleMessage("Hồ sơ"),
    "sign_in": MessageLookupByLibrary.simpleMessage("Đăng nhập"),
    "sub_title": MessageLookupByLibrary.simpleMessage(
      "Xây dựng, Quản lý cơ sở vật chất",
    ),
    "system_setting": MessageLookupByLibrary.simpleMessage("Cài đặt hệ thống"),
    "talk_hub": MessageLookupByLibrary.simpleMessage("Talk Hub"),
    "tickets": MessageLookupByLibrary.simpleMessage("Phiếu"),
    "user_info": m1,
    "version": MessageLookupByLibrary.simpleMessage("Phiên bản"),
  };
}
