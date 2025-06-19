<template>
	<div class="sys-onlineUser-container">
		<el-drawer v-model="state.isVisible" size="35%">
			<template #header>
				<div style="color: #fff">
					<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-UserFilled /> </el-icon>
					<span> {{ $t('message.list.onlineUserList') }} </span>
				</div>
			</template>
			<el-card shadow="hover" :body-style="{ padding: '5px 5px 0 5px', display: 'flex', width: '100%', height: '100%', alignItems: 'start' }">
				<el-form :model="state.queryParams" ref="queryForm" :show-message="false" :inlineMessage="true" label-width="auto" style="flex: 1 1 0%">
					<el-row :gutter="10">
						<el-col class="mb5" :xs="24" :sm="12" :md="12" :lg="12" :xl="12">
							<el-form-item :label="$t('message.list.account')" prop="userName">
								<el-input v-model="state.queryParams.userName" :placeholder="$t('message.list.account')" clearable @keyup.enter.native="handleQuery(true)" />
							</el-form-item>
						</el-col>

						<el-col class="mb5" :xs="24" :sm="12" :md="12" :lg="12" :xl="12">
							<el-form-item :label="$t('message.list.realName')" prop="realName">
								<el-input v-model="state.queryParams.realName" :placeholder="$t('message.list.realName')" clearable @keyup.enter.native="handleQuery(true)" />
							</el-form-item>
						</el-col>
					</el-row>
				</el-form>

				<el-divider style="height: calc(100% - 5px); margin: 0 10px" direction="vertical" />

				<el-row>
					<el-col>
						<el-button-group>
							<el-button type="primary" icon="ele-Search" @click="handleQuery(true)" :loading="options.loading"> {{ $t('message.list.query') }} </el-button>
							<el-button icon="ele-Refresh" @click="resetQuery" :loading="options.loading"> {{ $t('message.list.reset') }} </el-button>
						</el-button-group>
					</el-col>
				</el-row>
			</el-card>

			<el-card class="full-table" shadow="hover" style="margin-top: 5px">
				<vxe-grid ref="xGrid" class="xGrid-style" v-bind="options" v-on="gridEvents">
					<template #toolbar_buttons> </template>
					<template #toolbar_tools> </template>
					<template #empty>
						<el-empty :image-size="200" />
					</template>
					<template #row_buttons="{ row }">
						<el-tooltip :content="$t('message.list.sendMessage')" placement="top">
							<el-button icon="ele-Position" text type="primary" @click="openSendMessage(row)"> </el-button>
						</el-tooltip>
						<el-tooltip :content="$t('message.list.forceOffline')" placement="top">
							<el-button icon="ele-CircleCloseFilled" text type="danger" v-auth="'sysOnlineUser/forceOffline'" @click="forceOffline(row)"> </el-button>
						</el-tooltip>
					</template>
				</vxe-grid>
			</el-card>
		</el-drawer>

		<SendMessage ref="sendMessageRef" :title="$t('message.list.sendMessage')" />
	</div>
</template>

<script lang="ts" setup>
import { onMounted, reactive, ref } from 'vue';
import { ElMessageBox, ElNotification } from 'element-plus';
import { VxeGridInstance, VxeGridListeners, VxeGridPropTypes } from 'vxe-table';
import { useVxeTable } from '/@/hooks/useVxeTableOptionsHook';
import { useThemeConfig } from '/@/stores/themeConfig';
import { storeToRefs } from 'pinia';
import { Local } from '/@/utils/storage';
import { useI18n } from 'vue-i18n';
import { throttle } from 'lodash-es';
import { signalR } from './signalR';

import SendMessage from '/@/views/system/onlineUser/component/sendMessage.vue';

import { getAPI, clearAccessTokens } from '/@/utils/axios-utils';
import { SysOnlineUserApi, SysAuthApi } from '/@/api-services/api';
import { SysOnlineUser, PageOnlineUserInput } from '/@/api-services/models';

const storesThemeConfig = useThemeConfig();
const { themeConfig } = storeToRefs(storesThemeConfig);
const { t } = useI18n();
const xGrid = ref<VxeGridInstance>();
const sendMessageRef = ref<InstanceType<typeof SendMessage>>();
const state = reactive({
	isVisible: false,
	queryParams: {
		userName: undefined,
		realName: undefined,
	},
	localPageParam: {
		pageSize: 50 as number,
		defaultSort: { field: 'orderNo', order: 'asc', descStr: 'desc' },
	},
	onlineUserList: [] as Array<SysOnlineUser>, // 在线用户列表
	lastUserState: {
		online: false,
		realName: '',
	}, // 最后接收的用户变更状态信息
});

// 本地存储参数
const localPageParamKey = 'localPageParam:sysOnlineUser';
// 表格参数配置
const options = useVxeTable<SysOnlineUser>(
	{
		id: 'sysOnlineUser',
		name: t('message.list.onlineUserList'),
		columns: [
			// { type: 'checkbox', width: 40, fixed: 'left' },
			{ type: 'seq', title: t('message.list.seq'), width: 50, fixed: 'left' },
			{ field: 'userName', title: t('message.list.account'), minWidth: 110, showOverflow: 'tooltip' },
			{ field: 'realName', title: t('message.list.realName'), minWidth: 110, showOverflow: 'tooltip' },
			{ field: 'ip', title: t('message.list.ipAddress'), minWidth: 100, showOverflow: 'tooltip' },
			{ field: 'browser', title: t('message.list.browser'), minWidth: 160, showOverflow: 'tooltip' },
			// { field: 'connectionId', title: '连接Id', minWidth: 160, showOverflow: 'tooltip', sortable: true },
			{ field: 'time', title: t('message.list.loginTime'), minWidth: 120, showOverflow: 'tooltip' },
			{ field: 'buttons', title: t('message.list.operation'), fixed: 'right', width: 100, showOverflow: true, slots: { default: 'row_buttons' } },
		],
	},
	// vxeGrid配置参数(此处可覆写任何参数)，参考vxe-table官方文档
	{
		// 代理配置
		proxyConfig: { autoLoad: true, ajax: { query: ({ page, sort }) => handleQueryApi(page, sort) } },
		// 排序配置
		sortConfig: { defaultSort: Local.get(localPageParamKey)?.defaultSort || state.localPageParam.defaultSort },
		// 分页配置
		pagerConfig: { pageSize: Local.get(localPageParamKey)?.pageSize || state.localPageParam.pageSize },
		// 工具栏配置
		toolbarConfig: { export: true },
	}
);

// 页面初始化
onMounted(async () => {
	state.localPageParam = Local.get(localPageParamKey) || state.localPageParam;
	// 在线用户列表
	signalR.off('OnlineUserList');
	signalR.on('OnlineUserList', async (data: any) => {
		state.onlineUserList = data.userList;
		state.lastUserState = {
			online: data.online,
			realName: data.realName,
		};

		// 开启上线下线通知
		if (themeConfig.value.onlineNotice) notificationThrottle();

		// // 自动查询一次
		// await handleQuery();
	});
	// 强制下线
	signalR.off('ForceOffline');
	signalR.on('ForceOffline', async (data: any) => {
		// console.log('强制下线', data);
		await signalR.stop();

		await getAPI(SysAuthApi).apiSysAuthLogoutPost();
		clearAccessTokens();
	});
});

// 通知提示节流
const notificationThrottle = throttle(
	function () {
		ElNotification({
			title: '提示',
			message: `${state.lastUserState.online ? `【${state.lastUserState.realName}】上线了` : `【${state.lastUserState.realName}】离开了`}`,
			type: `${state.lastUserState.online ? 'info' : 'error'}`,
			position: 'bottom-right',
		});
	},
	3000,
	{
		leading: true,
		trailing: false,
	}
);

// 打开页面
const openDrawer = async () => {
	state.isVisible = true;
	await handleQuery();
};

// 查询api
const handleQueryApi = async (page: VxeGridPropTypes.ProxyAjaxQueryPageParams, sort: VxeGridPropTypes.ProxyAjaxQuerySortCheckedParams) => {
	const params = Object.assign(state.queryParams, { page: page.currentPage, pageSize: page.pageSize, field: sort.field, order: sort.order, descStr: 'desc' }) as PageOnlineUserInput;
	return getAPI(SysOnlineUserApi).apiSysOnlineUserPagePost(params);
};

// 查询操作
const handleQuery = async (reset = false) => {
	options.loading = true;
	reset ? await xGrid.value?.commitProxy('reload') : await xGrid.value?.commitProxy('query');
	options.loading = false;
};

// 重置操作
const resetQuery = async () => {
	state.queryParams.userName = undefined;
	state.queryParams.realName = undefined;
	await handleQuery();
};

// 表格事件
const gridEvents: VxeGridListeners<SysOnlineUser> = {
	// 只对 pager-config 配置时有效，分页发生改变时会触发该事件
	async pageChange({ pageSize }) {
		state.localPageParam.pageSize = pageSize;
		Local.set(localPageParamKey, state.localPageParam);
	},
	// 当排序条件发生变化时会触发该事件
	async sortChange({ field, order }) {
		state.localPageParam.defaultSort = { field: field, order: order!, descStr: 'desc' };
		Local.set(localPageParamKey, state.localPageParam);
	},
};

// 发送消息
const openSendMessage = (row: any) => {
	sendMessageRef.value?.openDialog(row);
};

// 强制下线
const forceOffline = async (row: any) => {
	ElMessageBox.confirm(t('message.list.confirmKickAccount', { account: row.realName }), t('message.list.hint'), {
		confirmButtonText: t('message.list.confirm'),
		cancelButtonText: t('message.list.cancelButtonText'),
		type: 'warning',
	})
		.then(async () => {
			await signalR.send('ForceOffline', { connectionId: row.connectionId }).catch(function (err: any) {
				console.log(err);
			});
		})
		.catch(() => {});
};

// 导出对象
defineExpose({ openDrawer });
</script>

<style lang="scss" scoped>
:deep(.el-drawer__body) {
	padding: 5px;
	display: flex;
	flex-direction: column;
	height: 100%;
}
.full-table {
	flex: 1;

	:deep(.el-card__body) {
		height: 100%;
		display: flex;
		flex-direction: column;
	}
}
</style>
