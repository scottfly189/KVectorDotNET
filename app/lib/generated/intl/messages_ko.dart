// DO NOT EDIT. This is code generated via package:intl/generate_localized.dart
// This is a library that provides messages for a ko locale. All the
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
  String get localeName => 'ko';

  static String m0(name) => "안녕하세요, ${name}";

  static String m1(name) => "사용자 이름:${name}";

  final messages = _notInlinedMessages(_notInlinedMessages);
  static Map<String, Function> _notInlinedMessages(_) => <String, Function>{
    "about": MessageLookupByLibrary.simpleMessage("정보"),
    "account": MessageLookupByLibrary.simpleMessage("계정"),
    "account_info": MessageLookupByLibrary.simpleMessage("계정 정보"),
    "app_name": MessageLookupByLibrary.simpleMessage(
      "ProjectZoom 건축 시공 관리 시스템",
    ),
    "cancel": MessageLookupByLibrary.simpleMessage("취소"),
    "change_language": MessageLookupByLibrary.simpleMessage("언어 변경"),
    "confirm": MessageLookupByLibrary.simpleMessage("확인"),
    "daily_log": MessageLookupByLibrary.simpleMessage("일일 로그"),
    "forget_password": MessageLookupByLibrary.simpleMessage("비밀번호를 잊으셨나요?"),
    "help_and_feedback": MessageLookupByLibrary.simpleMessage("도움 및 피드백"),
    "hi": m0,
    "job": MessageLookupByLibrary.simpleMessage("작업"),
    "language": MessageLookupByLibrary.simpleMessage("언어 변경"),
    "login_fail": MessageLookupByLibrary.simpleMessage("로그인 실패, 다시 시도해주세요"),
    "logout": MessageLookupByLibrary.simpleMessage("로그아웃"),
    "logout_confirm": MessageLookupByLibrary.simpleMessage("정말 로그아웃하시겠습니까?"),
    "my": MessageLookupByLibrary.simpleMessage("프로필"),
    "password": MessageLookupByLibrary.simpleMessage("비밀번호"),
    "print_setting": MessageLookupByLibrary.simpleMessage("인쇄 설정"),
    "project": MessageLookupByLibrary.simpleMessage("프로젝트"),
    "register": MessageLookupByLibrary.simpleMessage("등록"),
    "register_now": MessageLookupByLibrary.simpleMessage("지금 등록"),
    "report": MessageLookupByLibrary.simpleMessage("보고서"),
    "schedule": MessageLookupByLibrary.simpleMessage("일정"),
    "search": MessageLookupByLibrary.simpleMessage("검색..."),
    "select_project": MessageLookupByLibrary.simpleMessage("프로젝트 선택"),
    "setting": MessageLookupByLibrary.simpleMessage("프로필"),
    "sign_in": MessageLookupByLibrary.simpleMessage("로그인"),
    "sub_title": MessageLookupByLibrary.simpleMessage("건축, 시설 관리"),
    "system_setting": MessageLookupByLibrary.simpleMessage("시스템 설정"),
    "talk_hub": MessageLookupByLibrary.simpleMessage("Talk Hub"),
    "tickets": MessageLookupByLibrary.simpleMessage("티켓"),
    "user_info": m1,
    "version": MessageLookupByLibrary.simpleMessage("버전"),
  };
}
