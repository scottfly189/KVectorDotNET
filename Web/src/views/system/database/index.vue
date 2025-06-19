<template>
	<div class="sys-database-container">
		<el-card shadow="hover" :body-style="{ padding: '5px 5px 0 5px', display: 'flex', width: '100%', height: '100%', alignItems: 'start' }">
			<el-form :model="state.queryParams" ref="queryForm" :show-message="false" :inlineMessage="true" label-width="auto" style="flex: 1 1 0%">
				<el-row :gutter="10">
					<el-col class="mb5" :xs="24" :sm="12" :md="8" :lg="6" :xl="6">
						<el-form-item label="库名" prop="configId">
							<el-select v-model="state.configId" placeholder="库名" filterable @change="handleQueryTable">
								<el-option v-for="item in state.dbData" :key="item.configId" :label="`${item.dbName}(${item.configId})`" :value="item.configId" />
							</el-select>
						</el-form-item>
					</el-col>
					<el-col class="mb5" :xs="24" :sm="12" :md="8" :lg="6" :xl="6">
						<el-form-item label="表名" prop="tableName">
							<el-select v-model="state.tableName" placeholder="表名" filterable clearable @change="handleQueryColumn">
								<el-option v-for="item in state.tableData" :key="item.name" :label="`${item.name}${item.description ? '[' + item.description + ']' : ''}`" :value="item.name" />
							</el-select>
						</el-form-item>
					</el-col>
				</el-row>
			</el-form>
		</el-card>

		<el-card class="full-table" shadow="hover" style="margin-top: 5px">
			<vxe-grid ref="xGrid" class="xGrid-style" v-bind="options">
				<template #toolbar_buttons>
					<el-button-group>
						<el-button icon="ele-Plus" type="primary" @click="handleAddTable"> 增加表 </el-button>
						<el-button icon="ele-Edit" type="primary" @click="showEditTable"> 编辑表 </el-button>
						<el-button icon="ele-Delete" type="danger" @click="handleDeleteTable"> 删除表 </el-button>
					</el-button-group>
					<el-button-group style="padding-left: 12px; padding-right: 12px">
						<el-button icon="ele-CirclePlus" @click="showAddColumn"> 增加列 </el-button>
						<el-button icon="ele-CirclePlus" @click="showGenDialog"> 生成实体 </el-button>
						<el-popover placement="bottom" title="🔔提示" :width="220" trigger="hover" content="如果是刚刚生成的实体，请重启后台服务后再生成种子。">
							<template #reference>
								<el-button icon="ele-CirclePlus" @click="showGenSeedDataDialog"> 生成种子 </el-button>
							</template>
						</el-popover>
					</el-button-group>
					<el-button icon="ele-Refresh" type="warning" @click="handleInitTableAndSeedData"> 初始化库表结构及种子数据 </el-button>
					<el-button icon="ele-View" type="primary" plain @click="visualTable"> 库表关系可视化 </el-button>
				</template>
				<template #toolbar_tools> </template>
				<template #empty>
					<el-empty :image-size="200" />
				</template>
				<template #row_isPrimarykey="{ row }">
					<el-tag type="success" v-if="row.isPrimarykey === true">是</el-tag>
					<el-tag type="info" v-else>否</el-tag>
				</template>
				<template #row_isIdentity="{ row }">
					<el-tag type="success" v-if="row.isIdentity === true">是</el-tag>
					<el-tag type="info" v-else>否</el-tag>
				</template>
				<template #row_isNullable="{ row }">
					<el-tag v-if="row.isNullable === true">是</el-tag>
					<el-tag type="info" v-else>否</el-tag>
				</template>
				<template #row_buttons="{ row }">
					<el-tooltip content="上移" placement="top">
						<el-button icon="ele-Top" size="small" text type="primary" @click="moveColumn(row, 'up')" :disabled="row.$index === 0"></el-button>
					</el-tooltip>
					<el-tooltip content="下移" placement="top">
						<el-button icon="ele-Bottom" size="small" text type="primary" @click="moveColumn(row, 'down')" :disabled="row.$index === state.columnData.length - 1"></el-button>
					</el-tooltip>
					<el-tooltip content="编辑" placement="top">
						<el-button icon="ele-Edit" text type="primary" @click="showEditColumn(row)"> </el-button>
					</el-tooltip>
					<el-tooltip content="删除" placement="top">
						<el-button icon="ele-Delete" text type="danger" @click="handleDeleteColumn(row)"> </el-button>
					</el-tooltip>
				</template>
			</vxe-grid>
		</el-card>

		<EditTable ref="editTableRef" @handleQueryTable="handleQueryTable" />
		<EditColumn ref="editColumnRef" @handleQueryColumn="handleQueryColumn" />
		<AddTable ref="addTableRef" @addTableSubmitted="addTableSubmitted" />
		<AddColumn ref="addColumnRef" @handleQueryColumn="handleQueryColumn" />
		<GenEntity ref="genEntityRef" :applicationNamespaces="state.appNamespaces" />
		<GenSeedData ref="genSeedDataRef" :applicationNamespaces="state.appNamespaces" />
		<InitTableAndSeedData ref="initTableAndSeedDataRef" />
	</div>
</template>

<script lang="ts" setup name="sysDatabase">
import { onMounted, reactive, ref } from 'vue';
import { ElMessageBox, ElMessage } from 'element-plus';
import { useRouter } from 'vue-router';
import { VxeGridInstance, VxeGridListeners } from 'vxe-table';
import { useVxeTable } from '/@/hooks/useVxeTableOptionsHook';

import EditTable from '/@/views/system/database/component/editTable.vue';
import EditColumn from '/@/views/system/database/component/editColumn.vue';
import AddTable from '/@/views/system/database/component/addTable.vue';
import AddColumn from '/@/views/system/database/component/addColumn.vue';
import GenEntity from '/@/views/system/database/component/genEntity.vue';
import GenSeedData from '/@/views/system/database/component/genSeedData.vue';
import InitTableAndSeedData from '/@/views/system/database/component/initTableAndSeedData.vue';

import { getAPI } from '/@/utils/axios-utils';
import { SysDatabaseApi, SysCodeGenApi } from '/@/api-services/api';
import { DbColumnOutput, DbTableInfo, DbColumnInput, DeleteDbTableInput, DeleteDbColumnInput, MoveDbColumnInput } from '/@/api-services/models';

const xGrid = ref<VxeGridInstance>();
const editTableRef = ref<InstanceType<typeof EditTable>>();
const editColumnRef = ref<InstanceType<typeof EditColumn>>();
const addTableRef = ref<InstanceType<typeof AddTable>>();
const addColumnRef = ref<InstanceType<typeof AddColumn>>();
const genEntityRef = ref<InstanceType<typeof GenEntity>>();
const genSeedDataRef = ref<InstanceType<typeof GenSeedData>>();
const initTableAndSeedDataRef = ref<InstanceType<typeof InitTableAndSeedData>>();
const router = useRouter();
const state = reactive({
	loading: false,
	dbData: [] as any,
	configId: '',
	tableData: [] as Array<DbTableInfo>,
	tableName: '',
	columnData: [] as Array<DbColumnOutput>,
	queryParams: {
		name: undefined,
		code: undefined,
	},
	visible: false,
	title: '',
	appNamespaces: [] as Array<String>, // 存储位置
});

// 表格参数配置
const options = useVxeTable<DbColumnOutput>(
	{
		id: 'sysDatabase',
		name: '库表信息',
		columns: [
			// { type: 'checkbox', width: 40, fixed: 'left' },
			{ field: 'seq', type: 'seq', title: '序号', width: 50, fixed: 'left' },
			{ field: 'dbColumnName', title: '字段名', minWidth: 200, showOverflow: 'tooltip' },
			{ field: 'dataType', title: '数据类型', minWidth: 120, showOverflow: 'tooltip' },
			{ field: 'isPrimarykey', title: '主键', minWidth: 70, slots: { default: 'row_isPrimarykey' } },
			{ field: 'isIdentity', title: '自增', minWidth: 70, slots: { default: 'row_isIdentity' } },
			{ field: 'isNullable', title: '可空', minWidth: 70, slots: { default: 'row_isNullable' } },
			{ field: 'length', title: '长度', minWidth: 80, showOverflow: 'tooltip' },
			{ field: 'decimalDigits', title: '精度', minWidth: 80, showOverflow: 'tooltip' },
			{ field: 'defaultValue', title: '默认值', minWidth: 80, showOverflow: 'tooltip' },
			{ field: 'columnDescription', title: '描述', minWidth: 200, showOverflow: 'tooltip' },
			{ field: 'buttons', title: '操作', fixed: 'right', width: 140, showOverflow: true, slots: { default: 'row_buttons' } },
		],
	},
	// vxeGrid配置参数(此处可覆写任何参数)，参考vxe-table官方文档
	{
		// 代理配置
		proxyConfig: { autoLoad: true, ajax: { query: () => handleQueryColumnApi() } },
		// 分页配置
		pagerConfig: { enabled: false },
		// 工具栏配置
		toolbarConfig: { export: true },
	}
);

// 页面初始化
onMounted(async () => {
	options.loading = true;
	let res = await getAPI(SysDatabaseApi).apiSysDatabaseListGet();
	state.dbData = res.data.result;

	let appNamesRes = await getAPI(SysCodeGenApi).apiSysCodeGenApplicationNamespacesGet();
	state.appNamespaces = appNamesRes.data.result as Array<string>;
	options.loading = false;
});

// 查询列api
const handleQueryColumnApi = async () => {
	if (state.tableName == '' || typeof state.tableName == 'undefined') {
		return;
	}
	return getAPI(SysDatabaseApi).apiSysDatabaseColumnListTableNameConfigIdGet(state.tableName, state.configId);
};

// 查询列操作
const handleQueryColumn = async () => {
	options.loading = true;
	await xGrid.value?.commitProxy('query');
	options.loading = false;
};

// 增加表
const addTableSubmitted = async (e: any) => {
	await handleQueryTable();
	state.tableName = e;
	await handleQueryColumn();
};

// 表查询操作
const handleQueryTable = async () => {
	state.tableName = '';
	xGrid.value?.loadData([]);
	options.loading = true;
	var res = await getAPI(SysDatabaseApi).apiSysDatabaseTableListConfigIdGet(state.configId);
	let tableData = res.data.result ?? [];
	state.tableData = [];
	tableData.forEach((element: any) => {
		// 排除zero_开头的表
		if (!element.name.startsWith('zero_')) {
			state.tableData.push(element);
		}
	});
	options.loading = false;
};

// 打开表编辑页面
const showEditTable = () => {
	if (state.configId == '' || state.tableName == '') {
		ElMessage({ type: 'error', message: `请选择库名和表名!` });
		return;
	}
	var res = state.tableData.filter((u: any) => u.name == state.tableName);
	var table: any = {
		configId: state.configId,
		tableName: state.tableName,
		oldTableName: state.tableName,
		description: res[0].description,
	};
	editTableRef.value?.openDialog(table);
};

// 打开实体生成页面
const showGenDialog = () => {
	if (state.configId == '' || state.tableName == '') {
		ElMessage({ type: 'error', message: `请选择库名和表名!` });
		return;
	}
	// var res = state.tableData.filter((u: any) => u.name == state.tableName);
	var table: any = {
		configId: state.configId,
		tableName: state.tableName,
		position: state.appNamespaces[0],
	};
	genEntityRef.value?.openDialog(table);
};

// 生成种子数据页面
const showGenSeedDataDialog = () => {
	if (state.configId == '' || state.tableName == '') {
		ElMessage({ type: 'error', message: `请选择库名和表名!` });
		return;
	}
	var table: any = {
		configId: state.configId,
		tableName: state.tableName,
		position: state.appNamespaces[0],
	};
	genSeedDataRef.value?.openDialog(table);
};

// 打开表增加页面
const handleAddTable = () => {
	if (state.configId == '') {
		ElMessage({ type: 'error', message: `请选择库名!` });
		return;
	}
	var table: any = {
		configId: state.configId,
		tableName: '',
		oldTableName: '',
		description: '',
	};
	addTableRef.value?.openDialog(table);
};

// 打开列编辑页面
const showEditColumn = (row: any) => {
	var column: any = {
		configId: state.configId,
		tableName: row.tableName,
		columnName: row.dbColumnName,
		oldColumnName: row.dbColumnName,
		description: row.columnDescription,
		defaultValue: row.defaultValue,
	};
	editColumnRef.value?.openDialog(column);
};

// 打开列增加页面
const showAddColumn = () => {
	if (state.configId == '' || state.tableName == '') {
		ElMessage({ type: 'error', message: `请选择库名和表名!` });
		return;
	}
	const addRow: DbColumnInput = {
		configId: state.configId,
		tableName: state.tableName,
		columnDescription: '',
		dataType: '',
		dbColumnName: '',
		decimalDigits: 0,
		isIdentity: 0,
		isNullable: 0,
		isPrimarykey: 0,
		length: 0,
		// key: 0,
		// editable: true,
		// isNew: true,
	};
	addColumnRef.value?.openDialog(addRow);
};

// 删除表
const handleDeleteTable = () => {
	if (state.tableName == '') {
		ElMessage({ type: 'error', message: `请选择表名!` });
		return;
	}
	ElMessageBox.confirm(`确定删除表：【${state.tableName}】?`, '提示', {
		confirmButtonText: '确定',
		cancelButtonText: '取消',
		type: 'warning',
	})
		.then(async () => {
			const deleteDbTableInput: DeleteDbTableInput = {
				configId: state.configId,
				tableName: state.tableName,
			};
			await getAPI(SysDatabaseApi).apiSysDatabaseDeleteTablePost(deleteDbTableInput);
			await handleQueryTable();
			ElMessage.success('表删除成功');
		})
		.catch(() => {});
};

// 删除列
const handleDeleteColumn = (row: any) => {
	ElMessageBox.confirm(`确定删除列：【${row.dbColumnName}】?`, '提示', {
		confirmButtonText: '确定',
		cancelButtonText: '取消',
		type: 'warning',
	})
		.then(async () => {
			const eleteDbColumnInput: DeleteDbColumnInput = {
				configId: state.configId,
				tableName: state.tableName,
				dbColumnName: row.dbColumnName,
			};
			await getAPI(SysDatabaseApi).apiSysDatabaseDeleteColumnPost(eleteDbColumnInput);
			await handleQueryColumn();
			ElMessage.success('列删除成功');
		})
		.catch(() => {});
};

// 初始化库表结构及种子数据
const handleInitTableAndSeedData = () => {
	if (state.configId == '') {
		ElMessage({ type: 'error', message: `请选择库名!` });
		return;
	}
	initTableAndSeedDataRef.value?.openDialog(state.configId);
};

// 上移下移列顺序
const moveColumn = (row: any, direction: 'up' | 'down') => {
	const { columnData, tableName, configId } = state;
	const currentIndex = columnData.findIndex((item) => item.dbColumnName === row.dbColumnName);

	// 边界检查与反馈
	if (direction === 'up' && currentIndex === 0) {
		ElMessage.warning('已处于首位，无法上移');
		return;
	}
	if (direction === 'down' && currentIndex === columnData.length - 1) {
		ElMessage.warning('已处于末位，无法下移');
		return;
	}

	// 计算目标位置
	const targetIndex = direction === 'up' ? currentIndex - 1 : currentIndex + 1;
	const targetColumn = columnData[targetIndex];
	const columnName = direction === 'up' ? targetColumn.dbColumnName : row.dbColumnName;
	const afterColumnName = direction === 'up' ? row.dbColumnName : targetColumn.dbColumnName;

	ElMessageBox.confirm(`确定将列【${row.dbColumnName}】${direction === 'up' ? '上移' : '下移'}?`, '操作确认', {
		confirmButtonText: '确定',
		cancelButtonText: '取消',
		type: 'warning',
	})
		.then(async () => {
			try {
				const moveParams: MoveDbColumnInput = {
					configId,
					tableName,
					columnName,
					afterColumnName,
				};

				// 调用API
				await getAPI(SysDatabaseApi).apiSysDatabaseMoveColumnPost(moveParams);

				handleQueryColumn();
				ElMessage.success('列位置已更新');
			} catch (error: any) {
				ElMessage.error(`操作失败: ${error.message || '未知错误'}`);
			}
		})
		.catch(() => {});
};

// 表格事件
const gridEvents: VxeGridListeners<DbColumnOutput> = {
	async proxyQuery() {
		state.columnData = xGrid.value?.getTableData().tableData ?? [];
	},
};

// 可视化表
const visualTable = () => {
	if (state.configId == '') {
		ElMessage({ type: 'error', message: `请选择库名!` });
		return;
	}
	router.push(`/develop/database/visual?configId=${state.configId}`);
};
</script>
