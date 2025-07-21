// DO NOT EDIT. This is code generated via package:intl/generate_localized.dart
// This is a library that provides messages for a pt locale. All the
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
  String get localeName => 'pt';

  static String m0(name) => "Olá, ${name}";

  static String m1(name) => "Nome de usuário:${name}";

  final messages = _notInlinedMessages(_notInlinedMessages);
  static Map<String, Function> _notInlinedMessages(_) => <String, Function>{
    "about": MessageLookupByLibrary.simpleMessage("Sobre"),
    "account": MessageLookupByLibrary.simpleMessage("Conta"),
    "account_info": MessageLookupByLibrary.simpleMessage(
      "Informações da conta",
    ),
    "app_name": MessageLookupByLibrary.simpleMessage(
      "ProjectZoom Gerenciamento de construção e instalações",
    ),
    "cancel": MessageLookupByLibrary.simpleMessage("Cancelar"),
    "change_language": MessageLookupByLibrary.simpleMessage("Alterar idioma"),
    "confirm": MessageLookupByLibrary.simpleMessage("Confirmar"),
    "daily_log": MessageLookupByLibrary.simpleMessage("Diário"),
    "forget_password": MessageLookupByLibrary.simpleMessage(
      "Esqueceu sua senha?",
    ),
    "help_and_feedback": MessageLookupByLibrary.simpleMessage(
      "Ajuda e feedback",
    ),
    "hi": m0,
    "job": MessageLookupByLibrary.simpleMessage("Empregos"),
    "language": MessageLookupByLibrary.simpleMessage("Alterar idioma"),
    "login_fail": MessageLookupByLibrary.simpleMessage(
      "Login falhou, por favor tente novamente",
    ),
    "logout": MessageLookupByLibrary.simpleMessage("Sair"),
    "logout_confirm": MessageLookupByLibrary.simpleMessage(
      "Tem certeza que deseja sair?",
    ),
    "my": MessageLookupByLibrary.simpleMessage("Perfil"),
    "password": MessageLookupByLibrary.simpleMessage("Senha"),
    "print_setting": MessageLookupByLibrary.simpleMessage(
      "Configurações de impressão",
    ),
    "project": MessageLookupByLibrary.simpleMessage("Projeto"),
    "register": MessageLookupByLibrary.simpleMessage("Registrar"),
    "register_now": MessageLookupByLibrary.simpleMessage("Registrar agora"),
    "report": MessageLookupByLibrary.simpleMessage("Relatório"),
    "schedule": MessageLookupByLibrary.simpleMessage("Programação"),
    "search": MessageLookupByLibrary.simpleMessage("Pesquisar..."),
    "select_project": MessageLookupByLibrary.simpleMessage(
      "Selecione o projeto",
    ),
    "setting": MessageLookupByLibrary.simpleMessage("Perfil"),
    "sign_in": MessageLookupByLibrary.simpleMessage("Entrar"),
    "sub_title": MessageLookupByLibrary.simpleMessage(
      "Gestão de construção e instalações",
    ),
    "system_setting": MessageLookupByLibrary.simpleMessage(
      "Configurações do sistema",
    ),
    "talk_hub": MessageLookupByLibrary.simpleMessage("Talk Hub"),
    "tickets": MessageLookupByLibrary.simpleMessage("Bilhetes"),
    "user_info": m1,
    "version": MessageLookupByLibrary.simpleMessage("Versão"),
  };
}
