<template>
	<div class="user-news-container">
		<el-tabs stretch class="content-box">
			<el-tab-pane :label="$t('message.list.messageInbox')">
				<template #label>
					<el-icon><ele-Bell /></el-icon>
					<span style="margin-left: 5px">{{ $t('message.list.messageInbox') }}</span>
				</template>
				<div class="notice-box">
					<template v-if="noticeList.length > 0">
						<div class="notice-item" v-for="(v, k) in noticeList" :key="k" @click="viewNoticeDetail(v)" v-show="v.readStatus == 1 ? false : true">
							<div class="notice-title">{{ v.type == 1 ? '【' + $t('message.list.notice') + '】' : '【' + $t('message.list.announcement') + '】' }}{{ v.title }}</div>
							<div class="notice-content">{{ removeHtmlSub(v.content) }}</div>

							<div class="notice-time">{{ v.publicTime }}</div>
							<el-divider border-style="dashed" style="margin: 10px 0" />
						</div>
					</template>
					<el-empty :description="$t('message.list.empty')" v-else></el-empty>
				</div>
				<div class="notice-foot" @click="goToNotice" v-if="noticeList.length > 0">{{ $t('message.list.goToNotice') }}</div>
			</el-tab-pane>

			<el-tab-pane :label="$t('message.list.my')">
				<template #label>
					<el-icon><ele-Position /></el-icon>
					<span style="margin-left: 5px">{{ $t('message.list.my') }}</span>
				</template>
				<div style="height: 400px; overflow-y: auto; padding-right: 10px">
					<el-empty :description="$t('message.list.empty')"></el-empty>
				</div>
			</el-tab-pane>
		</el-tabs>
		<el-dialog v-model="state.dialogVisible" draggable width="769px">
			<template #header>
				<div style="color: #fff">
					<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-Bell /> </el-icon>
					<span> {{ $t('message.list.messageDetail') }} </span>
				</div>
			</template>
			<div class="w-e-text-container">
				<div v-html="state.content" data-slate-editor></div>
			</div>
			<template #footer>
				<span class="dialog-footer">
					<el-button type="primary" @click="state.dialogVisible = false"> {{ $t('message.list.confirm') }} </el-button>
				</span>
			</template>
		</el-dialog>
	</div>
</template>

<script setup lang="ts" name="layoutBreadcrumbUserNews">
import { reactive } from 'vue';
import router from '/@/router';
import commonFunction from '/@/utils/commonFunction';
import '@wangeditor/editor/dist/css/style.css';

import { getAPI } from '/@/utils/axios-utils';
import { SysNoticeApi } from '/@/api-services/api';

defineProps({
	noticeList: Array as any,
});
const { removeHtmlSub } = commonFunction();
const state = reactive({
	dialogVisible: false,
	content: '',
});
// 前往通知中心点击
const goToNotice = () => {
	router.push('/dashboard/notice');
};
// 查看消息详情
const viewNoticeDetail = async (notice: any) => {
	state.content = notice.content;
	state.dialogVisible = true;

	// 设置已读
	notice.readStatus = 1;
	await getAPI(SysNoticeApi).apiSysNoticeSetReadPost({ id: notice.id });
};
</script>

<style scoped lang="scss">
.user-news-container {
	.content-box {
		font-size: 12px;
		.notice-box {
			height: 400px;
			padding-right: 10px;

			margin-bottom: 35px;
			&:hover {
				overflow-y: scroll;
			}
		}
		.notice-item {
			&:hover {
				background-color: rgba(#b8b8b8, 0.1);
			}
			// .notice-title {
			// 	color: var(--el-color-primary);
			// }
			.notice-content {
				color: var(--el-text-color-secondary);
				margin-top: 3px;
				margin-bottom: 3px;
			}
			.notice-time {
				color: var(--el-text-color-secondary);
				text-align: right;
			}
		}
	}
	.notice-foot {
		height: 35px;
		width: 100%;
		color: var(--el-color-primary);
		font-size: 14px;
		cursor: pointer;
		position: absolute;
		bottom: 0px;
		background-color: #fff;
		display: flex;
		align-items: center;
		justify-content: center;
	}
}
:deep(.el-dialog__body) {
	min-height: 700px !important;
}
</style>
