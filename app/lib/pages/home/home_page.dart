import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:projectzoom/controllers/app_controller.dart';
import 'package:projectzoom/generated/l10n.dart';
import 'package:projectzoom/pages/chats/group_chat_page.dart';
import 'package:projectzoom/pages/diary/diary_page.dart';
import 'package:projectzoom/pages/job/job_page.dart';
import 'package:projectzoom/pages/schedule/schedule_page.dart';
import 'package:projectzoom/pages/ticket/ticket_page.dart';

/*
  首页框架
*/
class HomePageFramework extends StatefulWidget {
  const HomePageFramework({super.key});

  @override
  State<HomePageFramework> createState() => _HomePageFrameworkState();
}

class _HomePageFrameworkState extends State<HomePageFramework> {
  int currentPageIndex = 0;

  final List<Widget> pages = <Widget>[
    JobPage(),
    TicketsPage(),
    SchedulePage(),
    DiaryPage(),
    GroupChatPage(),
  ];

  @override
  Widget build(BuildContext context) {
    return GetBuilder<AppController>(
      init: AppController.to,
      builder: (controller) {
        final List<NavigationDestination> destinations = [
          NavigationDestination(
            icon: const Icon(Icons.assignment),
            selectedIcon: const Icon(Icons.assignment),
            label: S.of(context).job,
          ),
          NavigationDestination(
            icon: const Icon(Icons.bug_report),
            selectedIcon: const Icon(Icons.bug_report),
            label: S.of(context).tickets,
          ),
          NavigationDestination(
            icon: const Icon(Icons.calendar_today),
            selectedIcon: const Icon(Icons.calendar_today),
            label: S.of(context).schedule,
          ),
          NavigationDestination(
            icon: const Icon(Icons.event_note),
            selectedIcon: const Icon(Icons.event_note),
            label: S.of(context).daily_log,
          ),
          NavigationDestination(
            icon: const Icon(Icons.chat),
            selectedIcon: const Icon(Icons.chat),
            label: S.of(context).talk_hub,
          ),
        ];

        return Scaffold(
          appBar: AppBar(
            title: Container(
              height: 35,
              padding: EdgeInsets.zero,
              width: double.infinity,
              constraints: const BoxConstraints(),
              margin: const EdgeInsets.symmetric(horizontal: 0),
              decoration: BoxDecoration(
                color: Colors.white.withOpacity(0.3),
                borderRadius: BorderRadius.circular(20),
              ),
              child: GestureDetector(
                onTap: () {
                  // 点击搜索框时不做任何操作，保持焦点
                },
                child: Builder(
                  builder: (context) {
                    return Focus(
                      child: TextField(
                        autofocus: true,
                        decoration: InputDecoration(
                          hintText: S.of(context).search,
                          hintStyle: const TextStyle(color: Colors.white70),
                          prefixIcon:
                              const Icon(Icons.search, color: Colors.white),
                          suffixIcon: IconButton(
                            icon: const Icon(
                              Icons.qr_code_scanner,
                              color: Colors.white,
                            ),
                            onPressed: () {
                              // 这里添加扫码功能
                              // 先隐藏键盘
                              FocusScope.of(context).unfocus();
                            },
                          ),
                          border: InputBorder.none,
                        ),
                        style: const TextStyle(color: Colors.white),
                        textAlignVertical: TextAlignVertical.center,
                        onChanged: (value) {
                          // 这里可以添加搜索逻辑
                        },
                      ),
                    );
                  },
                ),
              ),
            ),
            backgroundColor: const Color(0xFF4A90E2),
            elevation: 0,
            leading: IconButton(
              padding: EdgeInsets.zero,
              constraints: const BoxConstraints(),
              onPressed: () {
                FocusScope.of(context).unfocus();
                Scaffold.of(context).openEndDrawer();
              },
              icon: const Icon(
                Icons.menu,
                color: Colors.white,
              ),
            ),
            actions: [
              MessageItem(controller: controller),
            ],
          ),
          bottomNavigationBar: NavigationBar(
            destinations: destinations,
            indicatorColor: Colors.amber,
            selectedIndex: currentPageIndex,
            onDestinationSelected: (index) {
              setState(() {
                currentPageIndex = index;
              });
            },
          ),
          body: pages[currentPageIndex],
        );
      },
    );
  }
}

/// 消息数量
class MessageItem extends StatelessWidget {
  final AppController controller;
  const MessageItem({super.key, required this.controller});

  @override
  Widget build(BuildContext context) {
    return Builder(
      builder: (context) => IconButton(
        padding: EdgeInsets.zero,
        constraints: const BoxConstraints(),
        icon: Stack(
          children: [
            const Icon(Icons.notifications, color: Colors.white),
            if (controller.unreadMessageCount > 0)
              Positioned(
                right: 0,
                top: 0,
                child: Container(
                  padding: const EdgeInsets.all(2),
                  decoration: BoxDecoration(
                    color: Colors.red,
                    borderRadius: BorderRadius.circular(50),
                  ),
                  constraints: const BoxConstraints(
                    minWidth: 8,
                    minHeight: 8,
                  ),
                  child: Text(
                    '${controller.unreadMessageCount}',
                    style: const TextStyle(
                      color: Colors.white,
                      fontSize: 6,
                    ),
                    textAlign: TextAlign.center,
                  ),
                ),
              ),
          ],
        ),
        onPressed: () {
          FocusScope.of(context).unfocus();
        },
      ),
    );
  }
}
