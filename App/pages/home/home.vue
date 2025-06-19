<template>
	<view>
		<up-swiper :list="state.swiperList" indicator indicatorMode="line" circular></up-swiper>

		<up-notice-bar bgColor="#fff" :text="state.notice"></up-notice-bar>

		<view class="grid grid-icon">
			<up-grid :border="false" col="4">
				<up-grid-item v-for="(item,index) in state.gridList" :key="index">
					<up-icon :customStyle="{color: 'white',paddingLeft:'20rpx'}" :name="item.name" :size="20" />
					<text class="grid-text">{{ item.title }}</text>
				</up-grid-item>
			</up-grid>
		</view>

		<SysLogOp />
	</view>

	<TabBar :current-page="0" />
</template>

<script lang="ts" setup>
	import { onMounted, reactive } from 'vue';
	import TabBar from '@/pages/component/tabbar.vue'
	import SysLogOp from '@/pages/component/sysLogOp.vue'

	const state = reactive({
		swiperList: [
			'/static/swiper/swiper1.png',
			'/static/uview/swiper/swiper2.png',
			'/static/uview/swiper/swiper3.png',
		],
		gridList: [
			{
				name: 'red-packet-fill',
				title: '功能1',
				url: '/'
			},
			{
				name: 'zhifubao-circle-fill',
				title: '功能2',
				url: '/'
			},
			{
				name: 'apple-fill',
				title: '功能3',
				url: '/'
			},
			{
				name: 'twitter-circle-fill',
				title: '功能4',
				url: '/'
			},
			{
				name: 'taobao-circle-fill',
				title: '功能5',
				url: '/'
			},
			{
				name: 'baidu-circle-fill',
				title: '测试6',
				url: '/'
			},
			{
				name: 'facebook-circle-fill',
				title: '功能7',
				url: '/'
			},
			{
				name: 'weibo-circle-fill',
				title: '功能8',
				url: '/'
			},
		],
		notice: '欢迎使用，Admin.NET 站在巨人肩膀上的 .NET 通用权限开发框架。让开发更简单、更通用、更流行！'
	})

	onMounted(() => {
	})
</script>

<style lang="scss" scoped>
	.grid {
		padding-top: 20rpx;
		background: #fff;
		box-sizing: border-box;
	}

	.grid-icon {
		::v-deep.u-icon__icon {
			color: white;
			width: 80rpx;
			height: 80rpx;
			border-radius: 50%;
			box-sizing: border-box;
			background: $uni-color-primary;
		}
	}

	.grid-text {
		font-size: 14px;
		padding: 10rpx 0 40rpx 0rpx;
	}
</style>