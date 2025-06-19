<template>
	<div class="sys-codeGenConfig-container">
		<vxe-modal v-model="state.isShowDialog" title="生成配置" :width="800" :height="350" show-footer show-zoom resize fullscreen @close="cancel">
			<template #default>
				<vxe-grid ref="xGrid" class="xGrid-table-style" v-bind="options">
					<template #drag_default="{}">
						<span class="drag-btn">
							<i class="fa fa-arrows"></i>
						</span>
					</template>
					<template #effectType="{ row, $index }">
						<vxe-select v-model="row.effectType" class="m-2" style="width: 70%" placeholder="Select" transfer :disabled="judgeColumns(row)" @change="effectTypeChange(row, $index)" filterable>
							<vxe-option v-for="item in state.effectTypeList" :key="item.value" :label="item.label" :value="item.value" />
						</vxe-select>
						<vxe-button v-if="row.effectType === 'ApiTreeSelector' || row.effectType === 'ForeignKey'" style="width: 30%" icon="vxe-icon-edit" @click="effectTypeChange(row, $index)">修改</vxe-button>
					</template>
					<template #columnComment="{ row }">
						<vxe-input v-model="row.columnComment" autocomplete="off" />
					</template>
					<template #dictType="{ row }">
						<vxe-select v-model="row.dictTypeCode" class="m-2" :disabled="effectTypeEnable(row)" filterable transfer>
							<vxe-option v-for="item in state.dictTypeCodeList" :key="item.code" :label="item.name" :value="item.code" />
						</vxe-select>
					</template>
					<template #whetherTable="{ row }">
						<vxe-checkbox v-model="row.whetherTable"></vxe-checkbox>
					</template>
					<template #whetherAddUpdate="{ row }">
						<vxe-checkbox v-model="row.whetherAddUpdate" :disabled="judgeColumns(row)"></vxe-checkbox>
					</template>
					<template #whetherSortable="{ row }">
						<vxe-checkbox v-model="row.whetherSortable"></vxe-checkbox>
					</template>
					<template #whetherRequired="{ row }">
						<vxe-tag v-if="row.whetherRequired" status="success">是</vxe-tag>
						<vxe-tag v-else status="info">否</vxe-tag>
					</template>
					<template #statistical="{ row }">
						<vxe-switch v-model="row.statistical" open-label="是" close-label="否" :openValue="true" :closeValue="false"></vxe-switch>
					</template>
					<template #isGroupBy="{ row }">
						<vxe-switch v-model="row.isGroupBy" open-label="是" close-label="否" :openValue="true" :closeValue="false"></vxe-switch>
					</template>
					<template #queryWhether="{ row }">
						<vxe-switch v-model="row.queryWhether" open-label="是" close-label="否" :openValue="true" :closeValue="false"></vxe-switch>
					</template>
					<template #queryType="{ row }">
						<vxe-select v-model="row.queryType" class="m-2" placeholder="Select" :disabled="!row.queryWhether" filterable transfer>
							<vxe-option v-for="item in state.queryTypeList" :key="item.value" :label="item.label" :value="item.value" />
						</vxe-select>
					</template>
					<template #verification="{ row }">
						<vxe-button status="primary" plain v-if="row.columnKey === 'False' && !row.whetherCommon" @click="openVerifyDialog(row)">校验规则{{ row.ruleCount }}</vxe-button>
						<span v-else></span>
					</template>
				</vxe-grid>
			</template>
			<template #footer>
				<vxe-button icon="ele-CircleCloseFilled" @click="cancel">取 消</vxe-button>
				<vxe-button status="primary" icon="ele-CircleCheckFilled" @click="submit">确 定</vxe-button>
			</template>
		</vxe-modal>

		<fkDialog ref="fkDialogRef" @submitRefreshFk="submitRefreshFk" />
		<treeDialog ref="treeDialogRef" @submitRefreshFk="submitRefreshFk" />
		<verifyDialog ref="verifyDialogRef" @submitVerify="submitVerifyOk" />
	</div>
</template>

<script lang="ts" setup name="sysCodeGenConfig">
import { nextTick, onMounted, onUnmounted, reactive, ref } from 'vue';
import mittBus from '/@/utils/mitt';
import Sortable from 'sortablejs';
import { VxeGridInstance, VxeGridProps } from 'vxe-pc-ui';

import fkDialog from '/@/views/system/codeGen/component/fkDialog.vue';
import treeDialog from '/@/views/system/codeGen/component/treeDialog.vue';
import verifyDialog from '/@/views/system/codeGen/component/verifyDialog.vue';

import { getAPI } from '/@/utils/axios-utils';
import { SysCodeGenConfigApi, SysConstApi, SysDictDataApi, SysDictTypeApi, SysEnumApi } from '/@/api-services/api';
// import { CodeGenConfig } from '/@/api-services/models/code-gen-config';

const xGrid = ref<VxeGridInstance<any>>();
const emits = defineEmits(['handleQuery']);
const fkDialogRef = ref();
const treeDialogRef = ref();
const verifyDialogRef = ref();
const state = reactive({
	isShowDialog: false,
	loading: false,
	EntityName: '',
	ConfigId: '',
	dbData: [] as any,
	effectTypeList: [] as any,
	dictTypeCodeList: [] as any,
	dictDataAll: [] as any,
	queryTypeList: [] as any,
	allConstSelector: [] as any,
	allEnumSelector: [] as any,
	sortable: undefined as any,
});

// 表格参数配置
const options = reactive<VxeGridProps>({
	id: 'genConfigDialog',
	height: 'auto',
	keepSource: true,
	autoResize: true,
	loading: false,
	align: 'center',
	rowConfig: { useKey: true },
	seqConfig: { seqMethod: ({ row }) => row.orderNo },
	columns: [
		{
			width: 50,
			slots: {
				default: 'drag_default',
			},
		},
		{
			field: 'orderNo',
			title: '排序',
			minWidth: 80,
			showOverflow: 'tooltip',
		},
		{
			field: 'columnName',
			title: '字段',
			minWidth: 160,
			showOverflow: 'tooltip',
		},
		{
			field: 'columnComment',
			title: '描述',
			minWidth: 120,
			showOverflow: 'tooltip',
			slots: {
				edit: 'columnComment',
				default: 'columnComment',
			},
		},
		{
			field: 'netType',
			title: '数据类型',
			minWidth: 90,
		},
		{
			field: 'effectType',
			title: '作用类型',
			minWidth: 160,
			slots: {
				edit: 'effectType',
				default: 'effectType',
			},
		},
		{
			field: 'dictTypeCode',
			title: '字典',
			minWidth: 180,
			slots: {
				edit: 'dictType',
				default: 'dictType',
			},
		},
		{
			field: 'whetherTable',
			title: '列表显示',
			minWidth: 70,
			slots: {
				edit: 'whetherTable',
				default: 'whetherTable',
			},
		},
		{
			field: 'whetherAddUpdate',
			title: '增改',
			minWidth: 70,
			slots: {
				edit: 'whetherAddUpdate',
				default: 'whetherAddUpdate',
			},
		},
		{
			field: 'whetherRequired',
			title: '必填',
			minWidth: 70,
			slots: {
				edit: 'whetherRequired',
				default: 'whetherRequired',
			},
		},
		{
			field: 'whetherSortable',
			title: '可排序',
			minWidth: 70,
			slots: {
				edit: 'whetherSortable',
				default: 'whetherSortable',
			},
		},
		{
			field: 'statistical',
			title: '统计字段',
			minWidth: 70,
			slots: {
				edit: 'statistical',
				default: 'statistical',
			},
		},
		{
			field: 'isGroupBy',
			title: 'GroupBy',
			minWidth: 70,
			slots: {
				edit: 'isGroupBy',
				default: 'isGroupBy',
			},
		},
		{
			field: 'queryWhether',
			title: '是否是查询',
			minWidth: 70,
			slots: {
				edit: 'queryWhether',
				default: 'queryWhether',
			},
		},
		{
			field: 'queryType',
			title: '查询方式',
			minWidth: 120,
			slots: {
				edit: 'queryType',
				default: 'queryType',
			},
		},
		{
			title: '校验规则',
			width: 130,
			showOverflow: true,
			slots: {
				edit: 'verification',
				default: 'verification',
			},
		},
	],
	editConfig: { trigger: 'click', mode: 'row', showStatus: true },
});

const rowDrop = () => {
	const el = document.querySelector('.xGrid-table-style .vxe-table--body tbody') as HTMLElement;
	state.sortable = Sortable.create(el, {
		animation: 300,
		handle: '.drag-btn',
		onEnd: (sortableEvent: any) => {
			const fullData = xGrid.value?.getTableData().fullData || [];
			const newIndex = sortableEvent.newIndex as number;
			const oldIndex = sortableEvent.oldIndex as number;
			var orderNo = fullData[newIndex - 1].orderNo;
			fullData[newIndex].orderNo = orderNo! + 10;
			const currentRow = fullData.splice(oldIndex, 1)[0];
			fullData.splice(newIndex, 0, currentRow);
			fullData.forEach((u, i) => (u.orderNo = 100 + i * 10));
			// 更新表格数据
			xGrid.value?.loadData(fullData);
		},
	});
};

// 页面初始化
onMounted(async () => {
	// 从后端获取字典列表数据，并保存到本地状态管理中
	var res = await getAPI(SysDictDataApi).apiSysDictDataDataListCodeGet('code_gen_effect_type');
	state.effectTypeList = res.data.result;

	// 获取字典类型代码列表，并将其保存到本地状态管理中，同时更新字典数据全集
	var res1 = await getAPI(SysDictTypeApi).apiSysDictTypeListGet();
	state.dictTypeCodeList = res1.data.result;
	state.dictDataAll = res1.data.result;

	// 获取查询类型列表数据，并保存到本地状态管理中
	var res2 = await getAPI(SysDictDataApi).apiSysDictDataDataListCodeGet('code_gen_query_type');
	state.queryTypeList = res2.data.result;

	// 从后端获取常量列表数据，并保存到本地状态管理中
	var res3 = await getAPI(SysConstApi).apiSysConstListGet();
	state.allConstSelector = res3.data.result;

	// 获取枚举类型列表数据，对其进行处理后保存到本地状态管理中
	let resEnum = await getAPI(SysEnumApi).apiSysEnumEnumTypeListGet();
	state.allEnumSelector = resEnum.data.result?.map((item) => ({ ...item, name: `${item.typeDescribe} [${item.typeName?.replace('Enum', '')}]`, code: item.typeName }));

	mittBus.on('submitRefreshFk', (data: any) => {
		let tableData = xGrid.value?.getData() || [];
		tableData[data.index] = data;
		xGrid.value?.loadData(tableData);
	});
});

// 更新主键
const submitRefreshFk = (data: any) => {
	let tableData = xGrid.value?.getData() || [];
	tableData[data.index] = data;
	xGrid.value?.reloadData(tableData);
};

// 页面初始化
onUnmounted(() => {
	// mittBus.off('submitRefresh', () => {});
	mittBus.off('submitRefreshFk', () => {});
});

// 控件类型改变
const effectTypeChange = (data: any, index: number) => {
	let value = data.effectType;
	if (value === 'ForeignKey') {
		openFkDialog(data, index);
	} else if (value === 'ApiTreeSelector') {
		openTreeDialog(data, index);
	} else if (value === 'DictSelector') {
		data.dictTypeCode = '';
		state.dictTypeCodeList = state.dictDataAll;
	} else if (value === 'ConstSelector') {
		data.dictTypeCode = '';
		state.dictTypeCodeList = state.allConstSelector;
	} else if (value == 'EnumSelector') {
		data.dictTypeCode = '';
		state.dictTypeCodeList = state.allEnumSelector;
	}
};

// 查询操作
const handleQuery = async (row: any) => {
	state.loading = true;
	var res = await getAPI(SysCodeGenConfigApi).apiSysCodeGenConfigListGet(undefined, row.id);
	var data = res.data.result ?? [];
	let lstWhetherColumn = ['whetherTable', 'whetherAddUpdate', 'whetherRequired', 'whetherSortable']; //列表显示的checkbox
	data.forEach((item: any) => {
		for (const key in item) {
			if (item[key] === 'Y') {
				item[key] = true;
			}
			if (item[key] === 'N' || (lstWhetherColumn.includes(key) && item[key] === null)) {
				item[key] = false;
			}
		}
		// 验证规则相关
		let rules = new Array();
		if (item.rules != '' && item.rules !== null) {
			rules = JSON.parse(item.rules);
		}
		item.ruleCount = rules.length > 0 ? `（${rules.length}）` : '';
	});
	xGrid.value?.loadData(data);
	state.loading = false;
};

// 判断是否（用于是否能选择或输入等）
function judgeColumns(data: any) {
	return data.whetherCommon == true || data.columnKey === 'True';
}

function effectTypeEnable(data: any) {
	return !['Radio', 'Checkbox', 'DictSelector', 'ConstSelector', 'EnumSelector'].some((e: any) => e === data.effectType);
}

// 打开弹窗
const openDialog = async (addRow: any) => {
	state.isShowDialog = true;
	state.ConfigId = addRow.configId;
	state.EntityName = addRow.tableName;
	nextTick(async () => {
		await handleQuery(addRow);
		rowDrop();
	});
};

// 打开外键弹窗
const openFkDialog = (addRow: any, index: number) => {
	addRow.index = index;
	fkDialogRef.value.openDialog(addRow);
};

const openTreeDialog = (addRow: any, index: number) => {
	addRow.index = index;
	treeDialogRef.value.openDialog(addRow);
};

// 打开校验弹窗
const openVerifyDialog = (row: any) => {
	// handleQuery(addRow);
	// state.isShowDialog = true;
	verifyDialogRef.value.openDialog(row);
};

// 验证提交回调
const submitVerifyOk = (data: any) => {
	let tableData = xGrid.value?.getData() || [];
	for (let i = 0; i < tableData.length; i++) {
		if (tableData[i].id == data.id) {
			tableData[i].rules = data.rules;
			tableData[i].ruleCount = data.ruleCount;
			// 更新必填项
			let rules = new Array();
			if (data.rules != '' && data.rules !== null) {
				rules = JSON.parse(data.rules);
				let requiredRule = rules.find((t) => t.type === 'required');
				if (requiredRule) {
					tableData[i].whetherRequired = true;
				} else {
					tableData[i].whetherRequired = false;
				}
			}
			break;
		}
	}
	xGrid.value?.reloadData(tableData);
};

// 关闭弹窗
const closeDialog = () => {
	emits('handleQuery');
	cancel();
};

// 取消
const cancel = () => {
	xGrid.value?.loadData([]);
	state.isShowDialog = false;
	if (state.sortable) {
		state.sortable.destroy();
	}
};

// 提交
const submit = async () => {
	state.loading = true;
	let lst = xGrid.value?.getData() || [];
	let ignoreFields = ['remoteVerify', 'anyRule', 'columnKey'];
	lst.forEach((item: any) => {
		// 必填那一项转换
		for (var key in item) {
			if (item[key] === true && !ignoreFields.includes(key)) {
				item[key] = 'Y';
			}
			if (item[key] === false && !ignoreFields.includes(key)) {
				item[key] = 'N';
			}
		}
	});
	await getAPI(SysCodeGenConfigApi).apiSysCodeGenConfigUpdatePost(lst);
	state.loading = false;
	closeDialog();
};

// 导出对象
defineExpose({ openDialog });
</script>

<style lang="scss" scoped>
.xGrid-table-style .drag-btn {
	cursor: move;
	font-size: 20px;
}
</style>
