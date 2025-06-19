<template>
	<view>
		<view class="mineInfo">
			<view class="userInfo flex" style="justify-content: space-between;">
				<view class="flex-column">
					<image v-if="state.userInfo.avatar" class="avatar " :src="env.baseUrl + state.userInfo.avatar" />
					<view v-else class="avatar "> {{ state.userInfo.RealName?.slice(0, 1) }} </view>
				</view>

				<view class="flex flex-column" style="width: calc(100% - 150rpx);">
					<view class="userInfo-name"> {{ state.userInfo.Account }} </view>
					<view style="display: flex; align-items: flex-end;">
						{{ state.userInfo.RealName }}
						<up-tag class="mg-l-20" size="mini" color="#fff" border-color="#fff" plain text="标签" />
					</view>
				</view>
			</view>
		</view>

		<view class="pd-lr-20" style="margin-top: -80rpx;">
			<view class="mg-t-20">
				<blockquote class="grid grid-icon pd-t-20">
					<up-grid :border="false" col="4">
						<up-grid-item v-for="(item,index) in state.gridList" :key="index">
							<up-icon :customStyle="{color: 'white',paddingLeft:'20rpx'}" :name="item.name" :size="20" />
							<text class="grid-text">{{ item.title }}</text>
						</up-grid-item>
					</up-grid>
				</blockquote>
			</view>
		</view>

		<view style="background-color: #FFF; margin-top: 30rpx;">
			<up-cell-group :border="false">
				<up-cell icon="lock-opened-fill" title="修改密码" :isLink="true" url="/pages/password/password" />
				<up-cell icon="file-text-fill" title="关于我们" :isLink="true" url="/pages/about/about" />
				<up-cell icon="edit-pen-fill" title="意见反馈" :isLink="true" url="/pages/feedback/feedback" />
				<up-cell icon="integral-fill" title="当前版本" value="v1.0.0" />
				<up-cell icon="pushpin-fill" title="退出登录" @click="logout" />
			</up-cell-group>
		</view>
	</view>

	<TabBar :current-page="1" />

</template>

<script lang="ts" setup>
	import { onMounted, reactive } from 'vue';
	import { env } from '@/utils/.env';
	import TabBar from '@/pages/component/tabbar.vue'
	import { logoutApi } from '@/api/sysAuth.js'
	import { accessTokenKey, decryptJWT } from '@/utils/request.js';

	const state = reactive({
		theme: uni.getStorageSync('theme'),
		userInfo: {} as any,
		gridList: [
			{
				name: 'red-packet-fill',
				title: '功能1'
			},
			{
				name: 'zhifubao-circle-fill',
				title: '功能2'
			},
			{
				name: 'apple-fill',
				title: '功能3'
			},
			{
				name: 'twitter-circle-fill',
				title: '功能4'
			}
		],
	})

	// 页面初始化
	onMounted(() => {
		state.userInfo = decryptJWT(uni.getStorageSync(accessTokenKey))
		console.log(state.userInfo)

		if (state.userInfo == '') {
			state.userInfo = {
				'account': 'Admin.NET',
				'phone': '18012345678'
			}
		}
	});

	// 退出系统
	const logout = () => {
		uni.showModal({
			title: '提示',
			content: '是否退出系统？',
			success: (e) => {
				if (!e.confirm) return;

				logoutApi().then((res : any) => {
					if (res.code == 200) {
						uni.showToast({ title: '退出系统', icon: 'none' })
						uni.clearStorageSync();
						setTimeout(() => {
							uni.reLaunch({ url: '/pages/login/login' })
						}, 1000)
					}
				})
			}
		})
	}
</script>

<style lang="scss" scoped>
	.flex {
		display: flex;
		flex-wrap: wrap;
	}

	.flex-column {
		display: flex;
		flex-direction: column;
		justify-content: space-around;
	}

	.mg-l-20 {
		margin-left: 20rpx;
	}

	.pd-lr-20 {
		padding: 0 20rpx;
	}

	.mineInfo {
		width: 100vw;
		padding: 20rpx 0 100rpx;
		background: $uni-color-primary;

		.userInfo {
			width: 90%;
			height: 120rpx;
			padding: 40rpx 0;
			margin: auto;
			color: white;

			.avatar {
				color: #1661AB;
				background: #FFF;
				text-align: center;
				border-radius: 50%;
				flex: none;
				width: 120rpx;
				height: 120rpx;
				font-size: 50rpx;
				line-height: 120rpx;

				image {
					width: 100%;
					height: 100%;
				}
			}

			&-name {
				font-size: 40rpx;
				font-weight: bold;
				// letter-spacing: 1px;
			}
		}
	}

	.grid {
		padding-top: 30rpx;
		background: #fff;
		border-radius: 15rpx;
		box-sizing: border-box;
	}

	.grid-icon {
		::v-deep.u-icon__icon {
			color: white;
			width: 80rpx;
			height: 80rpx;
			border-radius: 50%;
			box-sizing: border-box;
			background: $uni-color-primary ;
		}
	}

	.grid-text {
		font-size: 14px;
		padding: 10rpx 0 30rpx 0rpx;
	}


	.flex-column {
		display: flex;
		flex-direction: column;
		justify-content: c;
		height: 100%;
	}
</style>