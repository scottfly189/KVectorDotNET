// DO NOT EDIT. This is code generated via package:intl/generate_localized.dart
// This is a library that provides messages for a fr locale. All the
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
  String get localeName => 'fr';

  static String m0(name) => "Bonjour, ${name}";

  static String m1(name) => "Nom d\'utilisateur:${name}";

  final messages = _notInlinedMessages(_notInlinedMessages);
  static Map<String, Function> _notInlinedMessages(_) => <String, Function>{
    "about": MessageLookupByLibrary.simpleMessage("À propos"),
    "account": MessageLookupByLibrary.simpleMessage("Compte"),
    "account_info": MessageLookupByLibrary.simpleMessage(
      "Informations du compte",
    ),
    "app_name": MessageLookupByLibrary.simpleMessage(
      "ProjectZoom Gestion de la construction et du matériel",
    ),
    "cancel": MessageLookupByLibrary.simpleMessage("Annuler"),
    "change_language": MessageLookupByLibrary.simpleMessage(
      "Changer de langue",
    ),
    "confirm": MessageLookupByLibrary.simpleMessage("Confirmer"),
    "daily_log": MessageLookupByLibrary.simpleMessage("Journal"),
    "forget_password": MessageLookupByLibrary.simpleMessage(
      "Mot de passe oublié?",
    ),
    "help_and_feedback": MessageLookupByLibrary.simpleMessage(
      "Aide et feedback",
    ),
    "hi": m0,
    "job": MessageLookupByLibrary.simpleMessage("Emplois"),
    "language": MessageLookupByLibrary.simpleMessage("Changer de langue"),
    "login_fail": MessageLookupByLibrary.simpleMessage(
      "Erreur de connexion, veuillez réessayer",
    ),
    "logout": MessageLookupByLibrary.simpleMessage("Déconnexion"),
    "logout_confirm": MessageLookupByLibrary.simpleMessage(
      "Êtes-vous sûr de vouloir vous déconnecter?",
    ),
    "my": MessageLookupByLibrary.simpleMessage("Profil"),
    "password": MessageLookupByLibrary.simpleMessage("Mot de passe"),
    "print_setting": MessageLookupByLibrary.simpleMessage(
      "Paramètres d\'impression",
    ),
    "project": MessageLookupByLibrary.simpleMessage("Projet"),
    "register": MessageLookupByLibrary.simpleMessage("Inscription"),
    "register_now": MessageLookupByLibrary.simpleMessage(
      "Inscription maintenant",
    ),
    "report": MessageLookupByLibrary.simpleMessage("Miten"),
    "schedule": MessageLookupByLibrary.simpleMessage("Programmation"),
    "search": MessageLookupByLibrary.simpleMessage("Rechercher..."),
    "select_project": MessageLookupByLibrary.simpleMessage(
      "Sélectionner un projet",
    ),
    "setting": MessageLookupByLibrary.simpleMessage("Profil"),
    "sign_in": MessageLookupByLibrary.simpleMessage("Connexion"),
    "sub_title": MessageLookupByLibrary.simpleMessage(
      "Gestion de la construction et du matériel",
    ),
    "system_setting": MessageLookupByLibrary.simpleMessage(
      "Paramètres du système",
    ),
    "talk_hub": MessageLookupByLibrary.simpleMessage("Talk Hub"),
    "tickets": MessageLookupByLibrary.simpleMessage("Tickets"),
    "user_info": m1,
    "version": MessageLookupByLibrary.simpleMessage("Version"),
  };
}
