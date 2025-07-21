import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:go_router/go_router.dart';
import 'package:projectzoom/generated/l10n.dart';
import 'package:projectzoom/pages/setting/controllers/setting_controller.dart';
import 'package:projectzoom/services/user_config.dart';

class SettingPage extends StatelessWidget {
  const SettingPage({super.key});

  @override
  Widget build(BuildContext context) {
    final gNavigator = GoRouter.of(context);
    return Scaffold(
      body: GetBuilder<SettingController>(
        init: SettingController(),
        builder: (controller) => Column(
          children: [],
          //
          //   children: [
          //     // 顶部个人信息区域
          //     Container(
          //       padding: const EdgeInsets.only(
          //         top: 60,
          //         left: 24,
          //         right: 24,
          //         bottom: 24,
          //       ),
          //       decoration: BoxDecoration(
          //         gradient: LinearGradient(
          //           begin: Alignment.topCenter,
          //           end: Alignment.bottomCenter,
          //           stops: const [0.0, 0.3, 0.7, 1.0],
          //           colors: [
          //             Color(0xFF87CEEB), // 天蓝色
          //             Color(0xFFE6F3F8), // 淡蓝色
          //             Color(0xFFFFD700), // 金黄色
          //             Color(0xFFFFF8DC), // 玉米丝色
          //           ],
          //         ),
          //       ),
          //       width: double.infinity,
          //       child: Stack(
          //         alignment: Alignment.center,
          //         children: [
          //           // 添加装饰圆形
          //           Positioned(
          //             top: -50,
          //             right: -30,
          //             child: Container(
          //               width: 150,
          //               height: 150,
          //               decoration: BoxDecoration(
          //                 shape: BoxShape.circle,
          //                 color: Colors.white.withOpacity(0.1),
          //               ),
          //             ),
          //           ),
          //           Positioned(
          //             top: 20,
          //             left: -20,
          //             child: Container(
          //               width: 100,
          //               height: 100,
          //               decoration: BoxDecoration(
          //                 shape: BoxShape.circle,
          //                 color: Colors.white.withOpacity(0.15),
          //               ),
          //             ),
          //           ),
          //           Positioned(
          //             top: 40,
          //             right: 50,
          //             child: Container(
          //               width: 20,
          //               height: 20,
          //               decoration: BoxDecoration(
          //                 shape: BoxShape.circle,
          //                 color: Color(0xFFFFD700).withOpacity(0.3),
          //               ),
          //             ),
          //           ),
          //           // 个人信息内容
          //           Center(
          //             child: Column(
          //               mainAxisAlignment: MainAxisAlignment.center,
          //               children: [
          //                 Container(
          //                   padding: const EdgeInsets.all(4),
          //                   decoration: BoxDecoration(
          //                     shape: BoxShape.circle,
          //                     border: Border.all(
          //                       color: Colors.white.withOpacity(0.3),
          //                       width: 2,
          //                     ),
          //                   ),
          //                   child: CircleAvatar(
          //                     radius: 40,
          //                     backgroundColor: Colors.white,
          //                     child: Image.asset(
          //                       'assets/images/convert.png',
          //                       width: 45,
          //                       height: 45,
          //                     ),
          //                   ),
          //                 ),
          //                 const SizedBox(height: 16),
          //                 Text(
          //                   '${S.of(context).user_info(controller.userName.value)}',
          //                   style: const TextStyle(
          //                     color: Color(0xFF1A1A1A),
          //                     fontSize: 24,
          //                     fontWeight: FontWeight.bold,
          //                     shadows: [
          //                       Shadow(
          //                         offset: Offset(1, 1),
          //                         blurRadius: 3.0,
          //                         color: Colors.white,
          //                       ),
          //                     ],
          //                   ),
          //                 ),
          //               ],
          //             ),
          //           ),
          //         ],
          //       ),
          //     ),
          //     // 菜单列表
          //     Expanded(
          //       child: ListView(
          //         children: [
          //           _buildMenuItem(
          //             icon: Icons.account_circle_outlined,
          //             title: S.of(context).account_info,
          //             onTap: () {},
          //           ),
          //           _buildMenuItem(
          //             icon: Icons.language_outlined,
          //             title: S.of(context).change_language,
          //             onTap: () {
          //               controller.showLanguageDialog(context);
          //             },
          //           ),
          //           _buildMenuItem(
          //             icon: Icons.print_outlined,
          //             title: S.of(context).print_setting,
          //             onTap: () {},
          //           ),
          //           _buildMenuItem(
          //             icon: Icons.settings_outlined,
          //             title: S.of(context).system_setting,
          //             onTap: () {},
          //           ),
          //           _buildMenuItem(
          //             icon: Icons.help_outline,
          //             title: S.of(context).help_and_feedback,
          //             onTap: () {},
          //           ),
          //           _buildMenuItem(
          //             icon: Icons.logout,
          //             title: S.of(context).logout,
          //             showDivider: false,
          //             onTap: () {
          //               showDialog(
          //                 context: context,
          //                 builder: (context) => AlertDialog(
          //                   title: Text(S.current.logout),
          //                   content: Text(S.current.logout_confirm),
          //                   actions: [
          //                     TextButton(
          //                       onPressed: () => Navigator.of(context).pop(),
          //                       child: Text(S.current.cancel),
          //                     ),
          //                     TextButton(
          //                       onPressed: () {
          //                         Navigator.of(context).pop();
          //                         UserConfig.to.isLogin = false;
          //                         gNavigator.go('/login');
          //                       },
          //                       child: Text(S.current.confirm),
          //                     ),
          //                   ],
          //                 ),
          //               );
          //             },
          //           ),
          //         ],
          //       ),
          //     ),
          //   ],
        ),
      ),
    );
  }

  Widget _buildMenuItem({
    required IconData icon,
    required String title,
    bool showDivider = true,
    required VoidCallback onTap,
  }) {
    return Column(
      mainAxisSize: MainAxisSize.min,
      children: [
        ListTile(
          leading: Icon(icon, color: const Color(0xFF4A90E2)),
          title: Text(title),
          trailing: const Icon(Icons.arrow_forward_ios,
              size: 16, color: Color(0xFF4A90E2)),
          onTap: onTap,
        ),
        if (showDivider) const Divider(height: 1, indent: 16, endIndent: 16),
      ],
    );
  }
}

class SettingBackgroundPainter extends CustomPainter {
  @override
  void paint(Canvas canvas, Size size) {
    final paint = Paint()
      ..color = Colors.white.withOpacity(0.1)
      ..style = PaintingStyle.stroke
      ..strokeWidth = 1.0;

    // 绘制波浪形装饰
    Path path = Path();
    for (double i = 0; i < size.width + 50; i += 50) {
      path.moveTo(i, 0);
      path.quadraticBezierTo(
        i + 25,
        25,
        i + 50,
        0,
      );
    }
    canvas.drawPath(path, paint);

    // 绘制点阵装饰
    final dotPaint = Paint()
      ..color = Colors.white.withOpacity(0.15)
      ..style = PaintingStyle.fill;

    for (double x = 20; x < size.width; x += 40) {
      for (double y = 40; y < size.height; y += 40) {
        canvas.drawCircle(Offset(x, y), 1.5, dotPaint);
      }
    }
  }

  @override
  bool shouldRepaint(covariant CustomPainter oldDelegate) => false;
}
