// DO NOT EDIT. This is code generated via package:intl/generate_localized.dart
// This is a library that provides messages for a en locale. All the
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
  String get localeName => 'en';

  static String m0(name) => "Hi, ${name}";

  static String m1(name) => "User Name:${name}";

  final messages = _notInlinedMessages(_notInlinedMessages);
  static Map<String, Function> _notInlinedMessages(_) => <String, Function>{
    "about": MessageLookupByLibrary.simpleMessage("About"),
    "account": MessageLookupByLibrary.simpleMessage("Account"),
    "account_info": MessageLookupByLibrary.simpleMessage("Account Info"),
    "app_name": MessageLookupByLibrary.simpleMessage(
      "ProjectZoom Construction Management System",
    ),
    "cancel": MessageLookupByLibrary.simpleMessage("Cancel"),
    "change_language": MessageLookupByLibrary.simpleMessage("Change Language"),
    "confirm": MessageLookupByLibrary.simpleMessage("Confirm"),
    "daily_log": MessageLookupByLibrary.simpleMessage("Daily Log"),
    "forget_password": MessageLookupByLibrary.simpleMessage("Forget password?"),
    "help_and_feedback": MessageLookupByLibrary.simpleMessage(
      "Help and Feedback",
    ),
    "hi": m0,
    "job": MessageLookupByLibrary.simpleMessage("Jobs"),
    "language": MessageLookupByLibrary.simpleMessage("Change Language"),
    "login_fail": MessageLookupByLibrary.simpleMessage(
      "Login failed, please try again",
    ),
    "logout": MessageLookupByLibrary.simpleMessage("Logout"),
    "logout_confirm": MessageLookupByLibrary.simpleMessage(
      "Are you sure to logout?",
    ),
    "my": MessageLookupByLibrary.simpleMessage("Profile"),
    "password": MessageLookupByLibrary.simpleMessage("Password"),
    "print_setting": MessageLookupByLibrary.simpleMessage("Print Setting"),
    "project": MessageLookupByLibrary.simpleMessage("Project"),
    "register": MessageLookupByLibrary.simpleMessage("Register"),
    "register_now": MessageLookupByLibrary.simpleMessage("Register now"),
    "report": MessageLookupByLibrary.simpleMessage("Report"),
    "schedule": MessageLookupByLibrary.simpleMessage("Schedule"),
    "search": MessageLookupByLibrary.simpleMessage("Search..."),
    "select_project": MessageLookupByLibrary.simpleMessage("Select Project"),
    "setting": MessageLookupByLibrary.simpleMessage("Profile"),
    "sign_in": MessageLookupByLibrary.simpleMessage("Sign in"),
    "sub_title": MessageLookupByLibrary.simpleMessage(
      "Construction, Facility Management",
    ),
    "system_setting": MessageLookupByLibrary.simpleMessage("System Setting"),
    "talk_hub": MessageLookupByLibrary.simpleMessage("Talk Hub"),
    "tickets": MessageLookupByLibrary.simpleMessage("Tickets"),
    "user_info": m1,
    "version": MessageLookupByLibrary.simpleMessage("Version"),
  };
}
