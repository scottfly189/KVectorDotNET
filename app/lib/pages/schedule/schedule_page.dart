import 'package:flutter/material.dart';
import 'package:projectzoom/generated/l10n.dart';

class SchedulePage extends StatelessWidget {
  const SchedulePage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Text(S.current.schedule),
      ),
    );
  }
}
