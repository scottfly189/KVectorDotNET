<template>
	<div class="sys-codeGenConfig-container">
		<el-dialog v-model="state.isShowDialog" draggable :close-on-click-modal="false" width="800px">
			<template #header>
				<div style="color: #fff">
					<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-Edit /> </el-icon>
					<span> {{ state.title }} </span>
				</div>
			</template>
			<div class="tool-box">
				<el-button type="primary" icon="ele-Plus" @click="openRuleDialog"> 新增 </el-button>
			</div>
			<el-table :data="state.tableData" style="width: 100%" v-loading="state.loading" border>
				<el-table-column prop="type" label="类型" width="120" show-overflow-tooltip />
				<el-table-column prop="message" label="提示信息" minWidth="180" show-overflow-tooltip />
				<el-table-column prop="min" label="最小值" minWidth="100" show-overflow-tooltip />
				<el-table-column prop="max" label="最大值" minWidth="100" show-overflow-tooltip />
				<el-table-column prop="pattern" label="正则式" minWidth="120" show-overflow-tooltip />
				<el-table-column prop="action" label="操作" width="100" align="center" show-overflow-tooltip>
					<template #default="scope">
						<el-button icon="ele-Delete" text type="danger" @click="handleDeleteRule(scope)">删除</el-button>
					</template>
				</el-table-column>
			</el-table>
			<template #footer>
				<span class="dialog-footer">
					<el-button icon="ele-CircleCloseFilled" @click="cancel">取 消</el-button>
					<el-button type="primary" icon="ele-CircleCheckFilled" @click="submit">确 定</el-button>
				</span>
			</template>
		</el-dialog>
		<ruleDialog ref="ruleDialogRef" @submitRule="submitRuleOK" />
	</div>
</template>

<script lang="ts" setup>
import { reactive, ref, toRaw } from 'vue';
import ruleDialog from '/@/views/system/codeGen/component/ruleDialog.vue';

const emits = defineEmits(['submitVerify']);
const ruleDialogRef = ref();
const state = reactive({
	id: 0,
	isShowDialog: false,
	loading: false,
	tableData: [] as any,
	title: '',
	column: {} as any,
});

// 打开弹窗
const openDialog = (row: any) => {
	state.title = `校验规则 -- ${row.columnComment}`;
	state.tableData = new Array();
	if (row.rules != '') {
		state.tableData = JSON.parse(row.rules);
	}

	// state.tableData = row.rules;
	state.id = row.id;
	state.isShowDialog = true;
	state.column = row;
	// console.log('column',row);
};

// 打开验证规则弹窗
const openRuleDialog = () => {
	ruleDialogRef.value.openDialog(state.column);
};

// 关闭弹窗
const closeDialog = () => {
	state.isShowDialog = false;
};

// 取消
const cancel = () => {
	state.isShowDialog = false;
};

// 提交
const submit = async () => {
	let data = toRaw(state);
	let newData = Object.assign({}, data);
	let ruleCount = newData.tableData.length > 0 ? `（${newData.tableData.length}）` : '';
	emits('submitVerify', { id: newData.id, rules: JSON.stringify(newData.tableData), ruleCount: ruleCount });
	closeDialog();
};

// 添加返回
const submitRuleOK = (data: any) => {
	let row = toRaw(data);
	if (state.tableData === null) {
		state.tableData = [];
	}
	state.tableData.push(row);
};

// 删除验证规则
const handleDeleteRule = (scope: any) => {
	state.tableData.splice(scope.$index, 1);
};

// 导出对象
defineExpose({ openDialog });
</script>

<style lang="scss" scoped>
.tool-box {
	padding-bottom: 20px;
	display: flex;
	gap: 20px;
	align-items: center;
	// background: red;
}
</style>
