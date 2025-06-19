<template>
	<view style="height:100vh; background:#fff;">
		<view class="img-a">
			<view class="t-b">
				欢迎使用 <br />
				<div style="font-size: 55rpx">Admin.NET 开发框架</div>
			</view>
		</view>
		<view class="login-view">
			<view class="t-login">
				<form class="cl">
					<view class="t-a">
						<view class="txt">
							<up-icon name="account" size="22" /> <span style="margin-left: 4rpx;">账号</span>
						</view>
						<input name="account" placeholder="请输入账号" v-model="state.formData.account" />
					</view>
					<view class="t-a">
						<view class="txt">
							<up-icon name="lock" size="22" /> <span style="margin-left: 4rpx;">密码</span>
						</view>
						<input type="password" name="password" maxlength="18" placeholder="请输入密码" v-model="state.password" />
					</view>
					<view style="display: flex;">
						<input type="number" name="code" maxlength="2" placeholder="请输入验证码" v-model="state.formData.code" />
						<div class="captImg" :style="'background-image: url('+state.captchaImage+');'" @click="initCaptcha"></div>
					</view>

					<up-button @tap="login" text="登 录"></up-button>
				</form>

				<view class="t-e cl" v-if="true">
					<up-divider style="margin-top: 150rpx;" text="第三方账号登录" :dashed="true"></up-divider>

					<view class="t-g" @tap="wxLogin">
						<image src="https://zhoukaiwen.com/img/loginImg/wx.png"></image>
					</view>
					<view class="t-g" @tap="zfbLogin">
						<image src="https://zhoukaiwen.com/img/loginImg/qq.png"></image>
					</view>
				</view>

				<view class="mg-t-20 fixed">
					<view class="flex-center" @click="callPhone">
						<!-- <up-icon name="kefu-ermai" color="#999"></up-icon> -->
						联系电话：{{ state.servicePphone }}
					</view>
					<view class="flex-center copyright mg-t-10"> Copyright © 2024 Admin.NET All rights reserved. </view>
				</view>
			</view>
		</view>
	</view>
</template>

<script lang="ts" setup>
	import { reactive, onMounted } from 'vue';
	import { env } from '@/utils/.env.js'
	import { sm2 } from 'sm-crypto'
	import { accessTokenKey, refreshAccessTokenKey } from '@/utils/request.js'
	// import { getCaptchaApi, loginApi } from '@/api/auth.js'
	import { captchaApi, loginApi } from '@/api/sysAuth.js'

	const state = reactive({
		password: 'Admin.NET++010101',
		captchaImage: '', // 验证码
		formData: {
			account: 'superadmin',
			password: '',
			codeId: 0,
			code: 'string'
		},
		servicePphone: '18012345678', // 服务电话
	})

	// 页面初始化
	onMounted(() => {
		initCaptcha()
	});

	// 获取验证码
	const initCaptcha = () => {
		captchaApi().then((res : any) => {
			state.captchaImage = 'data:text/html;base64,' + res.result?.img;
			state.formData.codeId = res.result?.id;
		})
	}

	// 登录系统
	const login = async () => {
		// 添加主题色
		uni.setStorageSync('theme', '#11559c')

		// 密码加密传输
		var password = sm2.doEncrypt(state.password, env.smKey, 1);
		state.formData.password = password;

		try {
			loginApi(state.formData).then((res : any) => {
				uni.setStorageSync(accessTokenKey, res.result.accessToken)
				uni.setStorageSync(refreshAccessTokenKey, res.result.refreshToken)
				uni.switchTab({ url: '/pages/home/home' })
			})
		} finally {
			initCaptcha()
		}
	}

	// 拨打电话
	const callPhone = () => {
		uni.makePhoneCall({
			phoneNumber: state.servicePphone
		});
	}

	// 微信登录
	const wxLogin = () => {
		uni.showToast({ title: '点击了微信！', icon: 'none' });
	}

	// 支付宝登录
	const zfbLogin = () => {
		uni.showToast({ title: '点击了支付宝！', icon: 'none' });
	}
</script>

<style lang="scss" scoped>
	.captImg {
		width: 300rpx;
		height: 80rpx;
		background-size: cover;
	}

	.txt {
		display: flex;
		align-items: end;
		font-size: 32rpx;
		color: #666;
	}

	.flex-center {
		color: #999;
		font-size: 14rpx;
		text-decoration: none;
		// align-items: baseline;
		text-align: center;
		align-items: center;
	}

	.copyright {
		color: #999;
		font-size: 14rpx;
	}

	.img-a {
		width: 100%;
		height: 500rpx;
		background-image: url(/static/bg.jpg);
		background-size: 100%;
	}

	.reg {
		font-size: 28rpx;
		color: #fff;
		height: 90rpx;
		line-height: 90rpx;
		border-radius: 50rpx;
		font-weight: bold;
		background: #f5f6fa;
		color: #000000;
		text-align: center;
		margin-top: 30rpx;
	}

	.login-view {
		width: 100%;
		position: relative;
		margin-top: -80rpx;
		background-color: #ffffff;
		border-radius: 8% 8% 0% 0;
	}

	.t-login {
		width: 600rpx;
		margin: 0 auto;
		font-size: 28rpx;
		padding-top: 80rpx;
	}

	.t-login button {
		font-size: 28rpx;
		background: #2796f2;
		color: #fff;
		height: 90rpx;
		line-height: 90rpx;
		border-radius: 50rpx;
		font-weight: bold;
	}

	.t-login input {
		height: 90rpx;
		line-height: 90rpx;
		margin-bottom: 50rpx;
		border-bottom: 1px solid #e9e9e9;
		font-size: 28rpx;
	}

	.t-login .t-a {
		position: relative;
	}

	.t-b {
		text-align: left;
		font-size: 42rpx;
		color: #ffffff;
		padding: 200rpx 0 0 70rpx;
		font-weight: bold;
		line-height: 70rpx;
	}

	.t-login .t-c {
		position: absolute;
		right: 22rpx;
		top: 22rpx;
		background: #5677fc;
		color: #fff;
		font-size: 24rpx;
		border-radius: 50rpx;
		height: 50rpx;
		line-height: 50rpx;
		padding: 0 25rpx;
	}

	.t-login .t-d {
		text-align: center;
		color: #999;
		margin: 80rpx 0;
	}

	.t-login .t-e {
		text-align: center;
		margin: 80rpx auto 0;
	}

	.t-login .t-g {
		float: left;
		width: 50%;
	}

	.t-login .t-e image {
		width: 50rpx;
		height: 50rpx;
	}

	.t-login .uni-input-placeholder {
		color: #aeaeae;
	}

	.cl {
		zoom: 1;
	}

	.cl:after {
		clear: both;
		display: block;
		visibility: hidden;
		height: 0;
		content: '\20';
	}

	.fixed {
		width: 100vw;
		padding: 10rpx 20rpx 30rpx;
		background: #fff;
		box-sizing: border-box;
		position: fixed;
		bottom: 0;
		left: 0;
	}

	::v-deep.u-button--info {
		color: white !important;
		border-radius: 30px !important;
		background: $uni-color-primary !important;
	}
</style>