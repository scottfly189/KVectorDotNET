// DO NOT EDIT. This is code generated via package:intl/generate_localized.dart
// This is a library that provides messages for a de locale. All the
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
  String get localeName => 'de';

  static String m0(name) => "Hallo, ${name}";

  static String m1(name) => "Benutzername:${name}";

  final messages = _notInlinedMessages(_notInlinedMessages);
  static Map<String, Function> _notInlinedMessages(_) => <String, Function>{
    "about": MessageLookupByLibrary.simpleMessage("Über"),
    "account": MessageLookupByLibrary.simpleMessage("Konto"),
    "account_info": MessageLookupByLibrary.simpleMessage("Kontoinformationen"),
    "app_name": MessageLookupByLibrary.simpleMessage(
      "ProjectZoom Bauverwaltungssystem",
    ),
    "cancel": MessageLookupByLibrary.simpleMessage("Abbrechen"),
    "change_language": MessageLookupByLibrary.simpleMessage("Sprache ändern"),
    "confirm": MessageLookupByLibrary.simpleMessage("Bestätigen"),
    "daily_log": MessageLookupByLibrary.simpleMessage("Tagesprotokoll"),
    "forget_password": MessageLookupByLibrary.simpleMessage(
      "Passwort vergessen?",
    ),
    "help_and_feedback": MessageLookupByLibrary.simpleMessage(
      "Hilfe und Feedback",
    ),
    "hi": m0,
    "job": MessageLookupByLibrary.simpleMessage("Arbeitsplätze"),
    "language": MessageLookupByLibrary.simpleMessage("Sprache ändern"),
    "login_fail": MessageLookupByLibrary.simpleMessage(
      "Anmeldefehler, bitte erneut versuchen",
    ),
    "logout": MessageLookupByLibrary.simpleMessage("Abmelden"),
    "logout_confirm": MessageLookupByLibrary.simpleMessage(
      "Sind Sie sicher, dass Sie abgemeldet werden möchten?",
    ),
    "my": MessageLookupByLibrary.simpleMessage("Profil"),
    "password": MessageLookupByLibrary.simpleMessage("Passwort"),
    "print_setting": MessageLookupByLibrary.simpleMessage("Drucken"),
    "project": MessageLookupByLibrary.simpleMessage("Projekt"),
    "register": MessageLookupByLibrary.simpleMessage("Registrieren"),
    "register_now": MessageLookupByLibrary.simpleMessage("Jetzt registrieren"),
    "report": MessageLookupByLibrary.simpleMessage("Bericht"),
    "schedule": MessageLookupByLibrary.simpleMessage("Planung"),
    "search": MessageLookupByLibrary.simpleMessage("Suchen..."),
    "select_project": MessageLookupByLibrary.simpleMessage("Projekt auswählen"),
    "setting": MessageLookupByLibrary.simpleMessage("Profil"),
    "sign_in": MessageLookupByLibrary.simpleMessage("Anmelden"),
    "sub_title": MessageLookupByLibrary.simpleMessage(
      "Bau, Einrichtungsmanagement",
    ),
    "system_setting": MessageLookupByLibrary.simpleMessage(
      "Systemeinstellungen",
    ),
    "talk_hub": MessageLookupByLibrary.simpleMessage("Talk Hub"),
    "tickets": MessageLookupByLibrary.simpleMessage("Tickets"),
    "user_info": m1,
    "version": MessageLookupByLibrary.simpleMessage("Version"),
  };
}
