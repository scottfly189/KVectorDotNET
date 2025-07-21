import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:projectzoom/generated/l10n.dart';
import 'package:projectzoom/pages/ticket/controllers/ticket_controller.dart';

class TicketsPage extends StatelessWidget {
  const TicketsPage({super.key});

  @override
  Widget build(BuildContext context) {
    return GetBuilder<TicketsController>(
      init: TicketsController(),
      builder: (controller) => Scaffold(
        backgroundColor: Colors.grey[100],
        endDrawer: Drawer(
          width: 250,
          child: ListView(
            padding: EdgeInsets.zero,
            children: [
              SizedBox(
                height: 120,
                child: DrawerHeader(
                  decoration: BoxDecoration(
                    color: Color(0xFF9747FF),
                  ),
                  child: Text(
                    '👉 ${S.current.select_project}',
                    style: TextStyle(
                      color: Colors.white,
                      fontSize: 22,
                    ),
                  ),
                ),
              ),
              for (int i = 4; i <= 11; i++)
                ListTile(
                  title: Text(
                    '${S.current.project}-${i.toString().padLeft(3, '0')}',
                    style: TextStyle(
                      color: controller.selectedConsolidationStore.value ==
                              '北京-北清路-海淀-${i.toString().padLeft(3, '0')}'
                          ? Color(0xFF9747FF)
                          : Colors.black87,
                    ),
                    overflow: TextOverflow.ellipsis,
                    maxLines: 1,
                  ),
                  selected: controller.selectedConsolidationStore.value ==
                      '北京-北清路-海淀-${i.toString().padLeft(3, '0')}',
                  selectedTileColor: Color(0xFF9747FF).withOpacity(0.1),
                  leading: Icon(
                    Icons.store,
                    color: controller.selectedConsolidationStore.value ==
                            '北京-北清路-海淀-${i.toString().padLeft(3, '0')}'
                        ? Color(0xFF9747FF)
                        : Colors.grey,
                  ),
                  onTap: () {
                    controller.selectedConsolidationStore.value =
                        '北京-北清路-海淀-${i.toString().padLeft(3, '0')}';
                    controller.update();
                    Navigator.pop(context);
                  },
                ),
            ],
          ),
        ),
        body: SingleChildScrollView(
          child: Column(
            children: [],
          ),
        ),
      ),
    );
  }
}

class BackgroundPatternPainter extends CustomPainter {
  @override
  void paint(Canvas canvas, Size size) {
    final paint = Paint()
      ..color = Colors.white.withAlpha(38)
      ..style = PaintingStyle.stroke
      ..strokeWidth = 1.5;

    Path path = Path();
    // 添加对角线纹理
    for (double i = -size.height; i < size.width + size.height; i += 30) {
      path.moveTo(0, i);
      path.lineTo(i, 0);
    }

    // 添加反向对角线纹理，创造网格效果
    for (double i = 0; i < size.width + size.height; i += 60) {
      path.moveTo(i, size.height);
      path.lineTo(size.width, i);
    }

    canvas.drawPath(path, paint);

    // 添加一些装饰性圆点
    final dotPaint = Paint()
      ..color = Colors.white.withAlpha(51)
      ..style = PaintingStyle.fill;

    for (double x = 20; x < size.width; x += 60) {
      for (double y = 20; y < size.height; y += 60) {
        canvas.drawCircle(Offset(x, y), 2, dotPaint);
      }
    }
  }

  @override
  bool shouldRepaint(covariant CustomPainter oldDelegate) => false;
}
