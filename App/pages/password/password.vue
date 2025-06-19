<template>
	<view class="pd-lr-20">
		<up-gap height="20" bgColor="#0000"></up-gap>
		<view class="mg-t-20" style="display: flex;align-items: baseline;">
			<up-icon name="lock-fill" size="15" />
			<span style="margin-left: 4rpx;">原密码</span>
		</view>
		<up-input class="mg-t-20" v-model="state.formData.paswOld" border="surround" placeholder="输入原密码" />
		<up-gap height="20" bgColor="#0000"></up-gap>

		<view class="mg-t-20" style="display: flex;align-items: baseline;">
			<up-icon name="lock-fill" size="15" />
			<span style="margin-left: 4rpx;">新密码</span>
		</view>
		<up-input class="mg-t-20" v-model="state.paswNew" border="surround" placeholder="请输入新的密码"
			@blur="newCode"></up-input>
		<up-input class="mg-t-20" v-model="state.formData.paswNew" border="surround" placeholder="再次输入确认密码"
			@blur="newCode2"></up-input>

		<view class="txt">
			<view :class="state.isTrue?'':'red'"> {{state.tstxt}} </view>
		</view>
		<up-gap height="40" bgColor="#0000"></up-gap>
		<up-button class="mg-t-20" icon="checkmark-circle" iconColor="#fff" text="提交" @click="onsubmit"></up-button>
	</view>
</template>

<script lang="ts" setup>
	import { reactive } from 'vue';
	import { sm2 } from 'sm-crypto'
	import { env } from '@/utils/.env.js'
	import { changePwdApi } from '@/api/sysUser.js'

	const state = reactive({
		isTrue: false,
		tstxt: '',
		paswNew: '',
		formData: {} as any,
	})

	const newCode = () => {
		if (state.paswNew.length >= 6 && state.paswNew.length <= 15) {
			state.tstxt = ''
			state.isTrue = true
		} else {
			state.isTrue = false
			state.tstxt = '请输入6-15位，字母或数字'
		}
	}

	const newCode2 = () => {
		if (state.formData.paswNew != state.paswNew) {
			state.isTrue = false
			state.tstxt = '两次密码不一致'
		} else {
			state.tstxt = ''
			state.isTrue = true
		}
	}

	const onsubmit = () => {
		if (!state.isTrue) return uni.showToast({ title: state.tstxt, icon: 'none' })
		let publicKey = env.smKey
		state.formData.passwordOld = sm2.doEncrypt(state.formData.paswOld, publicKey, 1)
		state.formData.passwordNew = sm2.doEncrypt(state.formData.paswNew, publicKey, 1)
		changePwdApi(state.formData).then((res : any) => {
			if (res.code == 200) {
				uni.showToast({ title: '修改成功', icon: 'none' })
				setTimeout(() => {
					uni.navigateBack()
				}, 1000)
			}
		})
	}
</script>

<style lang="scss" scoped>
	::v-deep.u-icon__icon {
		margin-top: 2px;
		margin-right: 5px !important;
		font-size: 35rpx !important;
	}

	.red {
		color: red;
	}

	.pd-lr-20 {
		padding: 0 16rpx;
	}

	.mg-t-20 {
		margin-top: 20rpx;
	}

	::v-deep.u-button--info {
		color: white !important;
		width: 90%;
		border-radius: 30px !important;
		background: $uni-color-primary !important;
	}
</style>