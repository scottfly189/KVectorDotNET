import { defineStore } from 'pinia';

export const useThemeConfig = defineStore('themeConfig', {
	state: (): ThemeConfigState => ({
		themeConfig: {
			// 是否开启布局配置抽屉
			isDrawer: false,

			/**
			 * 全局主题
			 */
			// 默认 primary 主题颜色
			primary: '#0f59a4', //胭脂红:#f03f24 //飞燕草蓝:#0f59a4 //薄荷绿:#207f4c
			// 是否开启深色模式
			isIsDark: false,

			/**
			 * 顶栏设置
			 */
			// 默认顶栏导航背景颜色
			topBar: '#FFFFFF',
			// 默认顶栏导航字体颜色
			topBarColor: '#000000',
			// 是否开启顶栏背景颜色渐变
			isTopBarColorGradual: false,

			/**
			 * 菜单设置
			 */
			// 默认菜单导航背景颜色
			menuBar: '#FFFFFF',
			// 默认菜单导航字体颜色
			menuBarColor: '#000000',
			// 默认菜单高亮背景色
			menuBarActiveColor: 'var(--el-color-primary-light-7)',
			// 是否开启菜单背景颜色渐变
			isMenuBarColorGradual: false,

			/**
			 * 分栏设置
			 */
			// 默认分栏菜单背景颜色
			columnsMenuBar: '#2C3A49',
			// 默认分栏菜单字体颜色
			columnsMenuBarColor: '#F0F0F0',
			// 是否开启分栏菜单背景颜色渐变
			isColumnsMenuBarColorGradual: false,
			// 是否开启分栏菜单鼠标悬停预加载(预览菜单)
			isColumnsMenuHoverPreload: false,
			// 分栏Logo高度(px)
			columnsLogoHeight: 50,
			// 分栏菜单宽度(px)
			columnsMenuWidth: 70,
			// 分栏菜单高度(px)
			columnsMenuHeight: 50,

			/**
			 * 界面设置
			 */
			// 是否开启菜单水平折叠效果
			isCollapse: false,
			// 是否开启菜单手风琴效果
			isUniqueOpened: true,
			// 是否开启固定 Header
			isFixedHeader: true,
			// 初始化变量，用于更新菜单 el-scrollbar 的高度，请勿删除
			isFixedHeaderChange: false,
			// 是否开启经典布局分割菜单（仅经典布局生效）
			isClassicSplitMenu: false,
			// 是否开启自动锁屏
			isLockScreen: false,
			// 开启自动锁屏倒计时(s/秒)
			lockScreenTime: 300,

			/**
			 * 界面显示
			 */
			// 是否开启侧边栏 Logo
			isShowLogo: true,
			// 初始化变量，用于 el-scrollbar 的高度更新，请勿删除
			isShowLogoChange: false,
			// 是否开启 Breadcrumb，强制经典、横向布局不显示
			isBreadcrumb: true,
			// 是否开启 Tagsview
			isTagsview: true,
			// 是否开启 Breadcrumb 图标
			isBreadcrumbIcon: true,
			// 是否开启 Tagsview 图标
			isTagsviewIcon: true,
			// 是否开启 TagsView 缓存
			isCacheTagsView: true,
			// 是否开启 TagsView 拖拽
			isSortableTagsView: true,
			// 是否开启 TagsView 共用 -- 共用详情界面：tagsView只会出现一个；非共用详情界面：tagsView会出现多个
			isShareTagsView: true,
			// 是否开启 Footer 底部版权信息
			isFooter: true,
			// 是否开启灰色模式
			isGrayscale: false,
			// 是否开启色弱模式
			isInvert: false,
			// 是否开启水印
			isWatermark: false,
			// 水印文案
			watermarkText: 'KVectorDOTNET',

			/**
			 * 其它设置
			 */
			// Tagsview 风格：可选值"<tags-style-one|tags-style-four|tags-style-five>"，默认 tags-style-five
			// 定义的值与 `/src/layout/navBars/tagsView/tagsView.vue` 中的 class 同名
			tagsStyle: 'tags-style-one',
			// 主页面切换动画: Animate.css
			animation: 'fadeDown',
			// 分栏高亮风格：可选值"<columns-round|columns-card>"，默认 columns-round
			columnsAsideStyle: 'columns-round',
			// 分栏布局风格：可选值"<columns-horizontal|columns-vertical>"，默认 columns-horizontal
			columnsAsideLayout: 'columns-vertical',

			/**
			 * 布局切换
			 * 注意：为了演示，切换布局时，颜色会被还原成默认，代码位置：/@/layout/navBars/topBar/settings.vue
			 * 中的 `initSetLayoutChange(设置布局切换，重置主题样式)` 方法
			 */
			// 布局切换：可选值"<defaults|classic|transverse|columns>"，默认 defaults
			layout: 'columns',

			/**
			 * 后端控制路由
			 */
			// 是否开启后端控制路由
			isRequestRoutes: true,

			/**
			 * 全局网站标题 / 副标题
			 */
			// 网站主标题（菜单导航、浏览器当前网页标题）
			globalTitle: 'KVectorDotNET',
			// 网站副标题（登录页顶部文字）
			globalViceTitle: 'KVectorDotNET',
			// 网站副标题（登录页顶部文字）
			globalViceTitleMsg: 'A .NET framework for permissions, artificial intelligence, and internationalization',
			// 版权和备案文字
			copyright: 'Copyright © 2021-Present KVectorDotNET All rights reserved.',
			// 默认初始语言，可选值"<zh-CN|en|zh-TW>"，默认 zh-CN
			globalI18n: 'zh-CN',
			// 默认全局组件大小，可选值"<large|'default'|small>"，默认 'large'
			globalComponentSize: 'small',
			// 系统 logo 地址
			logoUrl: '',
			// Icp备案号
			icp: '省ICP备12345678号',
			// Icp地址
			icpUrl: 'https://beian.miit.gov.cn',
			// 是否开启多语言切换
			i18NSwitch: true,
			// 闲置超时时间
			idleTimeout: 0,
			// 上线下线通知
			onlineNotice: true,
		},
	}),
	actions: {
		setThemeConfig(data: ThemeConfigState) {
			this.themeConfig = data.themeConfig;
		},
	},
});
