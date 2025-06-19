<template>
	<view class="pd-lr-20" style="padding-bottom: 80px;">
		<block v-for="(item,index) in state.logData" :key="index">
			<view class="cardBox mg-t-20">
				<view style="display: flex;">
					<view class="cardBox-avatar">
						{{ item.realName?.slice(0, 1) || item.realName?.slice(0, 1)}}
					</view>
					<view class="cardBox-rightInf">
						<view class="cardBox-rightInf-fontSize " style="align-items: baseline;">
							<span> {{ item.realName }}</span>
							<up-tag class="mg-l-20" size="mini" borderColor="#ffeed4" bgColor="#ffeed4" plain :text="item.httpMethod" color="#FB8B05" />
						</view>
						<view style="font-size: 26rpx;color: #777;" class="mg-t-20">
							<view style="display: flex;align-items: baseline;">
								{{ item.location }} <span style="padding: 0 6rpx;"> | </span> {{ item.remoteIp }}
							</view>
						</view>

					</view>
				</view>

				<up-line dashed direction="row" color="#ccc" margin="20rpx 0"></up-line>
				<view class=" cardBox-content">
					<span class="gray"></span> {{item.displayTitle}}
				</view>
				<view class=" cardBox-content mg-t-20">
					<span class="gray"></span> {{item.requestUrl}}
				</view>
				<view class="mg-t-20">
					<up-tag :text="item.browser" borderColor="#cee3f9" bgColor="#cee3f9" color="#11559c" size="mini" plain></up-tag>
					<up-tag :text="item.createTime" borderColor="#cee3f9" bgColor="#cee3f9" color="#11559c" size="mini" plain style="margin-left: 20rpx;"></up-tag>
					<up-tag :text="item.elapsed + '3ms'" borderColor="#ffeed4" bgColor="#ffeed4" color="#FB8B05" size="mini" plain style="margin-left: 20rpx;" />
				</view>
			</view>
		</block>
	</view>
</template>

<script lang="ts" setup>
	import { onMounted, reactive } from 'vue';
	import { pageApi } from '@/api/sysLogOp.js'

	const state = reactive({
		theme: uni.getStorageSync('theme'),
		requestBody: {
			page: 0,
			pageSize: 10
		},
		logData: {} as any,
	})

	// 页面初始化
	onMounted(() => {
		pageApi(state.requestBody).then((res : any) => {
			state.logData = res.result.items;
		})
	});
</script>

<style lang="scss" scoped>
	.pd-lr-20 {
		padding: 0 20rpx
	}

	.cardBox {
		padding: 20rpx;
		background: #fff;
		border-radius: 15rpx;
		box-sizing: border-box;
	}

	.mg-t-20 {
		margin-top: 16rpx;
	}

	.cardBox-avatar {
		color: #1661AB;
		background: #F1F0ED;
		text-align: center;
		border-radius: 50%;
		flex: none;
		width: 120rpx;
		height: 120rpx;
		font-size: 45rpx;
		line-height: 120rpx;
	}

	.cardBox-rightInf {
		width: calc(100% - 130rpx);
		padding-left: 24rpx;
		display: flex;
		flex-direction: column;
		justify-content: space-around;
		overflow: hidden;

		.cardBox-rightInf-fontSize {
			font-size: 25rpx;
			font-weight: bold;
			display: flex;
			justify-content: space-between;
		}
	}

	.gray {
		color: #999;
	}

	.cardBox-name {
		color: #999;
		font-size: 20rpx;
	}

	.cardBox-leixing {
		color: $uni-color-primary;
		padding-top: 5rpx;
		font-size: 1rem;
	}

	.cardBox-content {
		color: #333;
		font-size: 26rpx;
	}
</style>