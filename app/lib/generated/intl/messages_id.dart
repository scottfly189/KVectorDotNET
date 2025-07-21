// DO NOT EDIT. This is code generated via package:intl/generate_localized.dart
// This is a library that provides messages for a id locale. All the
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
  String get localeName => 'id';

  static String m0(name) => "Hai, ${name}";

  static String m1(name) => "Nama Pengguna: ${name}";

  final messages = _notInlinedMessages(_notInlinedMessages);
  static Map<String, Function> _notInlinedMessages(_) => <String, Function>{
    "about": MessageLookupByLibrary.simpleMessage("Tentang"),
    "account": MessageLookupByLibrary.simpleMessage("Akun"),
    "account_info": MessageLookupByLibrary.simpleMessage("Informasi Akun"),
    "app_name": MessageLookupByLibrary.simpleMessage(
      "ProjectZoom Manajemen Konstruksi dan Fasilitas",
    ),
    "cancel": MessageLookupByLibrary.simpleMessage("Batal"),
    "change_language": MessageLookupByLibrary.simpleMessage("Ubah Bahasa"),
    "confirm": MessageLookupByLibrary.simpleMessage("Konfirmasi"),
    "daily_log": MessageLookupByLibrary.simpleMessage("Log Harian"),
    "forget_password": MessageLookupByLibrary.simpleMessage("Lupa kata sandi?"),
    "help_and_feedback": MessageLookupByLibrary.simpleMessage(
      "Bantuan dan Umpan Balik",
    ),
    "hi": m0,
    "job": MessageLookupByLibrary.simpleMessage("Pekerjaan"),
    "language": MessageLookupByLibrary.simpleMessage("Ubah Bahasa"),
    "login_fail": MessageLookupByLibrary.simpleMessage(
      "Login gagal, silakan coba lagi",
    ),
    "logout": MessageLookupByLibrary.simpleMessage("Keluar"),
    "logout_confirm": MessageLookupByLibrary.simpleMessage(
      "Apakah Anda yakin ingin keluar?",
    ),
    "my": MessageLookupByLibrary.simpleMessage("Profil"),
    "password": MessageLookupByLibrary.simpleMessage("Kata Sandi"),
    "print_setting": MessageLookupByLibrary.simpleMessage("Pengaturan Cetak"),
    "project": MessageLookupByLibrary.simpleMessage("Proyek"),
    "register": MessageLookupByLibrary.simpleMessage("Daftar"),
    "register_now": MessageLookupByLibrary.simpleMessage("Daftar sekarang"),
    "report": MessageLookupByLibrary.simpleMessage("Laporan"),
    "schedule": MessageLookupByLibrary.simpleMessage("Jadwal"),
    "search": MessageLookupByLibrary.simpleMessage("Cari..."),
    "select_project": MessageLookupByLibrary.simpleMessage("Pilih Proyek"),
    "setting": MessageLookupByLibrary.simpleMessage("Profil"),
    "sign_in": MessageLookupByLibrary.simpleMessage("Masuk"),
    "sub_title": MessageLookupByLibrary.simpleMessage(
      "Konstruksi, Manajemen Fasilitas",
    ),
    "system_setting": MessageLookupByLibrary.simpleMessage("Pengaturan Sistem"),
    "talk_hub": MessageLookupByLibrary.simpleMessage("Talk Hub"),
    "tickets": MessageLookupByLibrary.simpleMessage("Tiket"),
    "user_info": m1,
    "version": MessageLookupByLibrary.simpleMessage("Versi"),
  };
}
