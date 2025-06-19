<template>
	<div class="pluginCore-container">
		<el-card shadow="hover" :body-style="{ paddingBottom: '0' }">
			<el-form :model="state.queryParams" ref="queryForm" :inline="true">
				<el-form-item label="功能名称">
					<el-input v-model="state.queryParams.name" placeholder="功能名称" clearable />
				</el-form-item>
				<el-form-item>
					<el-button-group>
						<el-button type="primary" icon="ele-Search" @click="handleQuery" v-auth="'sysPlugin/page'"> 查询 </el-button>
						<el-button icon="ele-Refresh" @click="resetQuery"> 重置 </el-button>
					</el-button-group>
				</el-form-item>
				<el-form-item>
					<el-button type="primary" icon="ele-Plus" @click="openUploadDialog" v-auth="'sysFile/uploadFile'"> 上传 </el-button>
				</el-form-item>
			</el-form>
		</el-card>

		<el-card class="full-table" shadow="hover" style="margin-top: 8px">
			<el-table :data="state.pluginData" style="width: 100%" v-loading="state.loading" border>
				<el-table-column type="index" label="序号" width="55" align="center" fixed />
				<el-table-column prop="pluginId" label="PluginId" header-align="center" show-overflow-tooltip />
				<el-table-column prop="displayName" label="名称" header-align="center" show-overflow-tooltip />
				<el-table-column prop="description" label="描述" header-align="center" show-overflow-tooltip />
				<el-table-column prop="author" width="100" label="作者" header-align="center" show-overflow-tooltip />
				<el-table-column prop="version" width="100" label="版本" header-align="center" show-overflow-tooltip />
				<el-table-column prop="orderNo" width="70" label="排序" align="center" show-overflow-tooltip />
				<el-table-column label="状态" width="70" align="center" show-overflow-tooltip>
					<template #default="scope">
						<el-tag type="success" v-if="scope.row.status === 1">启用</el-tag>
						<el-tag type="danger" v-else>禁用</el-tag>
					</template>
				</el-table-column>
				<el-table-column prop="createTime" label="修改时间" align="center" show-overflow-tooltip />

				<el-table-column label="操作" width="210" align="center" fixed="right" show-overflow-tooltip>
					<template #default="scope">
						<el-button icon="ele-Delete" size="small" text type="danger" @click="delPlugin(scope.row)" v-auth="'sysPluginCore/delete'"> 卸载 </el-button>
						<el-button v-if="scope.row.status != 1" icon="ele-InfoFilled" size="small" text type="primary" @click="enablePlugin(scope.row)" v-auth="'sysPluginCore/enable'"> 启用 </el-button>
						<el-button v-if="scope.row.status == 1" icon="ele-InfoFilled" size="small" text type="primary" @click="disablePlugin(scope.row)" v-auth="'sysPluginCore/disable'"> 禁用 </el-button>
						<el-dropdown>
							<el-button icon="ele-MoreFilled" size="small" text type="primary" style="padding-left: 12px" />
							<template #dropdown>
								<el-dropdown-menu>
									<el-dropdown-item v-if="scope.row.status == 1" icon="ele-InfoFilled" @click="detailsPlugin(scope.row)" divided :disabled="!auth('sysPluginCore/details')">
										查看介绍
									</el-dropdown-item>
									<el-dropdown-item v-if="scope.row.status == 1" icon="ele-InfoFilled" @click="readmePlugin(scope.row)" :disabled="!auth('sysPluginCore/readme')"> 查看文档 </el-dropdown-item>
									<el-dropdown-item v-if="scope.row.status == 1" icon="ele-InfoFilled" @click="settingPlugin(scope.row)" :disabled="!auth('sysPluginCore/setting')"> 设置 </el-dropdown-item>
								</el-dropdown-menu>
							</template>
						</el-dropdown>
					</template>
				</el-table-column>
			</el-table>
			<el-pagination
				v-model:currentPage="state.tableParams.page"
				v-model:page-size="state.tableParams.pageSize"
				:total="state.tableParams.total"
				:page-sizes="[10, 20, 50, 100]"
				small
				background
				@size-change="handleSizeChange"
				@current-change="handleCurrentChange"
				layout="total, sizes, prev, pager, next, jumper"
			/>
		</el-card>

		<el-dialog v-model="state.dialogUploadVisible" :lock-scroll="false" draggable width="400px">
			<template #header>
				<div style="color: #fff">
					<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-UploadFilled /> </el-icon>
					<span> 上传文件 </span>
				</div>
			</template>
			<div>
				<el-upload ref="uploadRef" drag :auto-upload="false" :limit="1" :file-list="state.fileList" action="" :on-change="handleChange" accept=".zip,.nupkg">
					<el-icon class="el-icon--upload">
						<ele-UploadFilled />
					</el-icon>
					<div class="el-upload__text">将文件拖到此处，或<em>点击上传</em></div>
					<template #tip>
						<div class="el-upload__tip">请上传大小不超过 10MB 的文件</div>
					</template>
				</el-upload>
			</div>
			<template #footer>
				<span class="dialog-footer">
					<el-button @click="state.dialogUploadVisible = false">取消</el-button>
					<el-button type="primary" @click="uploadFile">确定</el-button>
				</span>
			</template>
		</el-dialog>

		<el-dialog v-model="state.dialogDetailVisible" draggable width="1000px">
			<template #header>
				<div style="color: #fff">
					<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-Document /> </el-icon>
					<span>插件详细</span>
				</div>
			</template>
			<div>
				<el-card class="box-card">
					<div class="rem-item">
						<span class="rem-label">PluginId:</span><span>{{ state.pluginDetailData.pluginId }}</span>
					</div>
					<div class="rem-item">
						<span class="rem-label">名称:</span><span>{{ state.pluginDetailData.displayName }}</span>
					</div>
					<div class="rem-item">
						<span class="rem-label">描述:</span><span>{{ state.pluginDetailData.description }}</span>
					</div>
					<div class="rem-item">
						<span class="rem-label">作者:</span><span>{{ state.pluginDetailData.author }}</span>
					</div>
					<div class="rem-item">
						<span class="rem-label">版本:</span
						><span
							><el-tag>{{ state.pluginDetailData.version }}</el-tag></span
						>
					</div>
				</el-card>
			</div>
		</el-dialog>

		<DetailsPluginDialog :title="state.editPluginTitle" ref="detailsPluginRef" />

		<EditPlugin ref="editPluginRef" :title="state.editPluginTitle" />
	</div>
</template>

<script lang="ts" setup name="sysPluginCore">
import { onMounted, reactive, ref } from 'vue';
import { ElMessageBox, ElMessage, UploadInstance } from 'element-plus';
import { auth } from '/@/utils/authFunction';

import EditPlugin from './component/editPluginSetting.vue';
import DetailsPluginDialog from './component/detailsPluginDialog.vue';

import { getAPI } from '/@/utils/axios-utils';
import { SysPluginCoreApi } from '/@/api-plugins/pluginCore/api';
import { SysPluginCore } from '/@/api-plugins/pluginCore/models';

const editPluginRef = ref<InstanceType<typeof EditPlugin>>();
const detailsPluginRef = ref<InstanceType<typeof DetailsPluginDialog>>();
const uploadRef = ref<UploadInstance>();
const state = reactive({
	loading: false,
	pluginData: [] as Array<SysPluginCore>,
	queryParams: {
		name: undefined,
	},
	tableParams: {
		page: 1,
		pageSize: 20,
		total: 0 as any,
	},
	editPluginTitle: '',
	dialogUploadVisible: false,
	fileList: [] as any,
	dialogDetailVisible: false,
	content: '',
	pluginDetailData: [] as any,
});

onMounted(async () => {
	await handleQuery();
});

// 查询操作
const handleQuery = async () => {
	state.loading = true;
	let params = Object.assign(state.queryParams, state.tableParams);
	var res = await getAPI(SysPluginCoreApi).apiSysPluginCorePagePost(params);
	state.pluginData = res.data.result?.items ?? [];
	state.tableParams.total = res.data.result?.total;
	state.loading = false;
};

// 重置操作
const resetQuery = () => {
	state.queryParams.name = undefined;
	handleQuery();
};

// // 打开新增页面
// const openAddPlugin = () => {
// 	state.editPluginTitle = '添加动态插件';
// 	editPluginCoreRef.value?.openDialog({ orderNo: 100, status: 1 });
// };

// // 打开编辑页面
// const openEditPlugin = (row: any) => {
// 	state.editPluginTitle = '编辑动态插件';
// 	editPluginCoreRef.value?.openDialog(row);
// };

// 开启
const enablePlugin = (row: any) => {
	ElMessageBox.confirm(`确定开启插件：【${row.displayName}】?`, '提示', {
		confirmButtonText: '确定',
		cancelButtonText: '取消',
		type: 'warning',
	})
		.then(async () => {
			await getAPI(SysPluginCoreApi).apiSysPluginCoreEnablePost({ id: row.id });
			handleQuery();
			ElMessage.success('开启成功');
		})
		.catch(() => {});
};
// 禁用
const disablePlugin = (row: any) => {
	ElMessageBox.confirm(`确定禁用插件：【${row.displayName}】?`, '提示', {
		confirmButtonText: '确定',
		cancelButtonText: '取消',
		type: 'warning',
	})
		.then(async () => {
			await getAPI(SysPluginCoreApi).apiSysPluginCoreDisablePost({ id: row.id });
			handleQuery();
			ElMessage.success('禁用成功');
		})
		.catch(() => {});
};
// 卸载当前行
const delPlugin = (row: any) => {
	ElMessageBox.confirm(`确定卸载动态插件：【${row.displayName}】?`, '提示', {
		confirmButtonText: '确定',
		cancelButtonText: '取消',
		type: 'warning',
	})
		.then(async () => {
			await getAPI(SysPluginCoreApi).apiSysPluginCoreDeletePost({ id: row.id });
			handleQuery();
			ElMessage.success('卸载成功');
		})
		.catch(() => {});
};
// 查看详情
const detailsPlugin = async (row: any) => {
	state.content = row.description;
	state.dialogDetailVisible = true;
	var res = await getAPI(SysPluginCoreApi).apiSysPluginCoreBaseInfoGet(row.id);
	state.pluginDetailData = res.data.result;
};

// 查看详情
const readmePlugin = (row: any) => {
	state.content = row.description;
	state.editPluginTitle = '文档';
	detailsPluginRef.value?.openDialog(row);
};
// 插件设置
const settingPlugin = (row: any) => {
	state.editPluginTitle = '插件设置';
	editPluginRef.value?.openDialog(row);
};

// 打开上传页面
const openUploadDialog = () => {
	state.fileList = [];
	state.dialogUploadVisible = true;
};

// 通过onChanne方法获得文件列表
const handleChange = (file: any, fileList: []) => {
	state.fileList = fileList;
};

// 上传
const uploadFile = async () => {
	if (state.fileList.length < 1) return;
	await getAPI(SysPluginCoreApi).apiSysFileUploadFilePostForm(state.fileList[0].raw);
	handleQuery();
	ElMessage.success('上传成功');
	state.dialogUploadVisible = false;
};

// 改变页面容量
const handleSizeChange = (val: number) => {
	state.tableParams.pageSize = val;
	handleQuery();
};

// 改变页码序号
const handleCurrentChange = (val: number) => {
	state.tableParams.page = val;
	handleQuery();
};
</script>
