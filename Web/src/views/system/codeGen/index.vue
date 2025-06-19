<template>
	<div class="sys-codeGen-container">
		<el-card shadow="hover" :body-style="{ padding: '5px 5px 0 5px', display: 'flex', width: '100%', height: '100%', alignItems: 'start' }">
			<el-form :model="state.queryParams" ref="queryForm" :show-message="false" :inlineMessage="true" label-width="auto" style="flex: 1 1 0%">
				<el-row :gutter="10">
					<el-col class="mb5" :xs="24" :sm="12" :md="8" :lg="6" :xl="6">
						<el-form-item label="业务名" prop="busName">
							<el-input placeholder="业务名" clearable @keyup.enter="handleQuery" v-model="state.queryParams.busName" @keyup.enter.native="handleQuery(true)" />
						</el-form-item>
					</el-col>
					<el-col class="mb5" :xs="24" :sm="12" :md="8" :lg="6" :xl="6">
						<el-form-item label="数据库表名" prop="tableName">
							<el-input placeholder="数据库表名" clearable @keyup.enter="handleQuery" v-model="state.queryParams.tableName" @keyup.enter.native="handleQuery(true)" />
						</el-form-item>
					</el-col>
				</el-row>
			</el-form>

			<el-divider style="height: calc(100% - 5px); margin: 0 10px" direction="vertical" />

			<el-row>
				<el-col>
					<el-button-group>
						<el-button type="primary" icon="ele-Search" @click="handleQuery(true)" :loading="options.loading"> 查询 </el-button>
						<el-button icon="ele-Refresh" @click="resetQuery" :loading="options.loading"> 重置 </el-button>
					</el-button-group>
				</el-col>
			</el-row>
		</el-card>

		<el-card class="full-table" shadow="hover" style="margin-top: 5px">
			<vxe-grid ref="xGrid" class="xGrid-style" v-bind="options" v-on="gridEvents">
				<template #toolbar_buttons>
					<el-button type="primary" icon="ele-Plus" @click="handleAdd"> 新增 </el-button>
				</template>
				<template #toolbar_tools> </template>
				<template #empty>
					<el-empty :image-size="200" />
				</template>
				<template #row_generateType="{ row }">
					<el-tag v-if="row.generateType == 100"> 下载压缩包 </el-tag>
					<el-tag v-else-if="row.generateType == 111"> 下载压缩包(前端) </el-tag>
					<el-tag v-else-if="row.generateType == 121"> 下载压缩包(后端) </el-tag>
					<el-tag v-else-if="row.generateType == 211"> 生成到本项目(前端) </el-tag>
					<el-tag v-else-if="row.generateType == 221"> 生成到本项目(后端) </el-tag>
					<el-tag type="danger" v-else> 生成到本项目 </el-tag>
				</template>
				<template #row_buttons="{ row }">
					<el-tooltip content="编辑" placement="top">
						<el-button icon="ele-Edit" text type="primary" @click="handleEdit(row)"> </el-button>
					</el-tooltip>
					<el-tooltip content="删除" placement="top">
						<el-button icon="ele-Delete" text type="danger" @click="handleDelete(row)"> </el-button>
					</el-tooltip>
					<el-tooltip content="配置" placement="top">
						<el-button icon="ele-Setting" text type="danger" @click="handleConfig(row)">配置</el-button>
					</el-tooltip>
					<el-tooltip content="预览" placement="top">
						<el-button icon="ele-Camera" text type="primary" @click="handlePreview(row)">预览</el-button>
					</el-tooltip>
					<el-tooltip content="开始生成" placement="top">
						<el-button icon="ele-Cpu" text type="primary" @click="handleGenerate(row)">生成</el-button>
					</el-tooltip>
				</template>
			</vxe-grid>
		</el-card>

		<EditCodeGenDialog ref="EditCodeGenRef" :title="state.title" :applicationNamespaces="state.applicationNamespaces" @handleQuery="handleQuery" />
		<CodeConfigDialog ref="CodeConfigRef" @handleQuery="handleQuery" />
		<PreviewDialog :title="state.title" ref="PreviewRef" />
	</div>
</template>

<script lang="ts" setup name="sysCodeGen">
import { onMounted, reactive, ref, defineAsyncComponent } from 'vue';
import { ElMessageBox, ElMessage, ElNotification } from 'element-plus';
import { VxeGridInstance, VxeGridListeners, VxeGridPropTypes } from 'vxe-table';
import { useVxeTable } from '/@/hooks/useVxeTableOptionsHook';
import { Local } from '/@/utils/storage';
import { downloadByUrl } from '/@/utils/download';

import EditCodeGenDialog from './component/editCodeGenDialog.vue';
import CodeConfigDialog from './component/genConfigDialog.vue';

import { getAPI } from '/@/utils/axios-utils';
import { SysCodeGenApi } from '/@/api-services/api';
import { SysCodeGen, PageCodeGenInput } from '/@/api-services/models';

const PreviewDialog = defineAsyncComponent(() => import('./component/previewDialog.vue'));
const xGrid = ref<VxeGridInstance>();
const EditCodeGenRef = ref<InstanceType<typeof EditCodeGenDialog>>();
const CodeConfigRef = ref<InstanceType<typeof CodeConfigDialog>>();
const PreviewRef = ref<InstanceType<typeof PreviewDialog>>();
const state = reactive({
	dbData: [] as any,
	configId: '',
	tableName: '',
	queryParams: {
		name: undefined,
		code: undefined,
		tableName: undefined,
		busName: undefined,
	},
	localPageParam: {
		pageSize: 50 as number,
		defaultSort: { field: 'id', order: 'asc', descStr: 'desc' },
	},
	visible: false,
	title: '',
	applicationNamespaces: [] as Array<string>,
});

// 本地存储参数
const localPageParamKey = 'localPageParam:sysCodeGen';
// 表格参数配置
const options = useVxeTable<SysCodeGen>(
	{
		id: 'sysCodeGen',
		name: '代码生成',
		columns: [
			// { type: 'checkbox', width: 40, fixed: 'left' },
			{ field: 'seq', type: 'seq', title: '序号', width: 60, fixed: 'left' },
			{ field: 'configId', title: '库定位器', minWidth: 200, showOverflow: 'tooltip' },
			{ field: 'tableName', title: '表名称', minWidth: 200, showOverflow: 'tooltip' },
			{ field: 'busName', title: '业务名', minWidth: 200, showOverflow: 'tooltip' },
			{ field: 'nameSpace', title: '命名空间', minWidth: 200, showOverflow: 'tooltip' },
			{ field: 'authorName', title: '作者姓名', minWidth: 200, showOverflow: 'tooltip' },
			{ field: 'generateType', title: '生成方式', minWidth: 140, showOverflow: 'tooltip', slots: { default: 'row_generateType' } },
			{ field: 'buttons', title: '操作', fixed: 'right', width: 280, showOverflow: true, slots: { default: 'row_buttons' } },
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

	// 获取命名空间集合
	let res = await getAPI(SysCodeGenApi).apiSysCodeGenApplicationNamespacesGet();
	state.applicationNamespaces = res.data.result as Array<string>;
});

// 查询api
const handleQueryApi = async (page: VxeGridPropTypes.ProxyAjaxQueryPageParams, sort: VxeGridPropTypes.ProxyAjaxQuerySortCheckedParams) => {
	const params = Object.assign(state.queryParams, { page: page.currentPage, pageSize: page.pageSize, field: sort.field, order: sort.order, descStr: 'desc' }) as PageCodeGenInput;
	return getAPI(SysCodeGenApi).apiSysCodeGenPagePost(params);
};

// 查询操作
const handleQuery = async (reset = false) => {
	options.loading = true;
	reset ? await xGrid.value?.commitProxy('reload') : await xGrid.value?.commitProxy('query');
	options.loading = false;
};

// 重置操作
const resetQuery = async () => {
	state.queryParams.busName = undefined;
	state.queryParams.tableName = undefined;
	await xGrid.value?.commitProxy('reload');
};

// 打开新增页面
const handleAdd = () => {
	state.title = '增加代码生成';
	EditCodeGenRef.value?.openDialog({
		authorName: 'Admin.NET',
		template: 'template',
		generateType: '200',
		printType: 'off',
		menuIcon: 'ele-Menu',
		pagePath: 'main',
		nameSpace: state.applicationNamespaces[0],
		generateMenu: false,
		isApiService: false,
	});
};

// 打开编辑页面
const handleEdit = (row: any) => {
	state.title = '编辑代码生成';
	EditCodeGenRef.value?.openDialog(row);
};

// 删除
const handleDelete = (row: any) => {
	ElMessageBox.confirm(`确定删除吗?`, '提示', {
		confirmButtonText: '确定',
		cancelButtonText: '取消',
		type: 'warning',
	})
		.then(async () => {
			await getAPI(SysCodeGenApi).apiSysCodeGenDeletePost([{ id: row.id }]);
			await handleQuery();
			ElMessage.success('操作成功');
		})
		.catch(() => {});
};

// 表格事件
const gridEvents: VxeGridListeners<SysCodeGen> = {
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

// 打开配置
const handleConfig = (row: any) => {
	CodeConfigRef.value?.openDialog(row);
};

// 开始生成代码
const handleGenerate = (row: any) => {
	ElMessageBox.confirm(`确定要生成【${row.tableName}】表吗?`, '提示', {
		confirmButtonText: '确定',
		cancelButtonText: '取消',
		type: 'warning',
	})
		.then(async () => {
			var res = await getAPI(SysCodeGenApi).apiSysCodeGenRunLocalPost(row);
			if (res.data.result != null && res.data.result.url != null) downloadByUrl({ url: res.data.result.url });
			await handleQuery();

			ElNotification({
				title: '提示',
				message: '生成成功，请重启项目以加载最新代码',
				type: 'success',
				position: 'bottom-right',
			});
		})
		.catch(() => {});
};

// 预览代码
const handlePreview = (row: any) => {
	state.title = '预览代码';
	PreviewRef.value?.openDialog(row);
};
</script>
