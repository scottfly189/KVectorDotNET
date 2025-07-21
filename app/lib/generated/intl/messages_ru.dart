// DO NOT EDIT. This is code generated via package:intl/generate_localized.dart
// This is a library that provides messages for a ru locale. All the
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
  String get localeName => 'ru';

  static String m0(name) => "Привет, ${name}";

  static String m1(name) => "Имя пользователя:${name}";

  final messages = _notInlinedMessages(_notInlinedMessages);
  static Map<String, Function> _notInlinedMessages(_) => <String, Function>{
    "about": MessageLookupByLibrary.simpleMessage("О"),
    "account": MessageLookupByLibrary.simpleMessage("Аккаунт"),
    "account_info": MessageLookupByLibrary.simpleMessage(
      "Информация об аккаунте",
    ),
    "app_name": MessageLookupByLibrary.simpleMessage(
      "ProjectZoom Управление строительством и инфраструктурой",
    ),
    "cancel": MessageLookupByLibrary.simpleMessage("Отмена"),
    "change_language": MessageLookupByLibrary.simpleMessage("Изменить язык"),
    "confirm": MessageLookupByLibrary.simpleMessage("Подтвердить"),
    "daily_log": MessageLookupByLibrary.simpleMessage("Дневник"),
    "forget_password": MessageLookupByLibrary.simpleMessage("Забыли пароль?"),
    "help_and_feedback": MessageLookupByLibrary.simpleMessage(
      "Помощь и обратная связь",
    ),
    "hi": m0,
    "job": MessageLookupByLibrary.simpleMessage("Задачи"),
    "language": MessageLookupByLibrary.simpleMessage("Изменить язык"),
    "login_fail": MessageLookupByLibrary.simpleMessage(
      "Вход не выполнен, пожалуйста, попробуйте снова",
    ),
    "logout": MessageLookupByLibrary.simpleMessage("Выйти"),
    "logout_confirm": MessageLookupByLibrary.simpleMessage(
      "Вы уверены, что хотите выйти?",
    ),
    "my": MessageLookupByLibrary.simpleMessage("Настройки"),
    "password": MessageLookupByLibrary.simpleMessage("Пароль"),
    "print_setting": MessageLookupByLibrary.simpleMessage("Настройки печати"),
    "project": MessageLookupByLibrary.simpleMessage("Проект"),
    "register": MessageLookupByLibrary.simpleMessage("Зарегистрироваться"),
    "register_now": MessageLookupByLibrary.simpleMessage(
      "Зарегистрироваться сейчас",
    ),
    "report": MessageLookupByLibrary.simpleMessage("Отчет"),
    "schedule": MessageLookupByLibrary.simpleMessage("Расписание"),
    "search": MessageLookupByLibrary.simpleMessage("Поиск..."),
    "select_project": MessageLookupByLibrary.simpleMessage("Выберите проект"),
    "setting": MessageLookupByLibrary.simpleMessage("Настройки"),
    "sign_in": MessageLookupByLibrary.simpleMessage("Войти"),
    "sub_title": MessageLookupByLibrary.simpleMessage(
      "Строительство и управление инфраструктурой",
    ),
    "system_setting": MessageLookupByLibrary.simpleMessage("Настройки системы"),
    "talk_hub": MessageLookupByLibrary.simpleMessage("Talk Hub"),
    "tickets": MessageLookupByLibrary.simpleMessage("Билеты"),
    "user_info": m1,
    "version": MessageLookupByLibrary.simpleMessage("Версия"),
  };
}
