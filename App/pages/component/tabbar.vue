<template>
	<view class="tabbar-container">
		<view class="tabbar-item" v-for="item in state.tabbarList" :class="[item.centerItem ? ' center-item' : '']" @click="changeItem(item)">
			<view class="item-top">
				<up-icon :color="state.currentItem==item.id?state.theme:''" :name="item.icon" size="25"></up-icon>
			</view>
			<view class="item-bottom" :class="[state.currentItem==item.id ? 'item-active' : '']">
				<text style="font-size: 30rpx;">{{ item.text }}</text>
			</view>
		</view>
	</view>
</template>

<script lang="ts" setup>
	import { onMounted, reactive } from 'vue';

	const props = defineProps({
		currentPage: Number,
	});

	const state = reactive({
		theme: uni.getStorageSync('theme'),
		currentItem: 0,
		tabbarList: [{
			id: 0,
			path: "/pages/home/home",
			icon: 'home',
			text: "首页",
			centerItem: false
		}, {
			id: 1,
			path: "/pages/mine/mine",
			icon: 'account',
			text: "我的",
			centerItem: false
		}]
	})

	onMounted(() => {
		state.currentItem = props.currentPage;
		uni.hideTabBar();
	});

	const changeItem = (item : any) => {
		uni.switchTab({ url: item.path });
	}
</script>

<style lang="scss" scoped>
	.tabbar-container {
		position: fixed;
		bottom: 0;
		left: 0;
		width: 100%;
		height: 110rpx;
		box-shadow: 0px 3px 20px rgba(0, 0, 0, 0.16);
		border-top: 1px;
		display: flex;
		align-items: center;
		padding: 5rpx 0;
		color: #999999;
		z-index: 200;
		background-color: #fff;
	}

	.tabbar-container .tabbar-item {
		width: 50%;
		height: 100rpx;
		display: flex;
		flex-direction: column;
		justify-content: center;
		align-items: center;
		text-align: center;
		background-color: #fff;
	}

	.item-active {
		color: $uni-color-primary;
	}
</style>