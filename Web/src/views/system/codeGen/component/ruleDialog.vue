<template>
	<div class="sys-codeGenConfig-container">
		<el-dialog v-model="state.isShowDialog" draggable :close-on-click-modal="false" width="800px">
			<template #header>
				<div style="color: #fff">
					<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-Edit /> </el-icon>
					<span> 编辑规则 </span>
				</div>
			</template>
			<el-form :model="state.ruleForm" ref="ruleFormRef" label-width="auto">
				<el-row :gutter="10">
					<el-form-item style="display: none !important">
						<el-input v-model="state.ruleForm.id" />
					</el-form-item>
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
						<el-form-item label=""> 本字段的数据库类型是：【{{ state.column.dataType }}】，.Net类型是：【{{ state.column.netType }}】 </el-form-item>
					</el-col>
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
						<el-form-item label="验证类型" prop="type" :rules="rules.type">
							<el-select v-model="state.ruleForm.type" placeholder="请选择类型" @change="handleTypeChange">
								<el-option v-for="(item, index) in validTypeData" :key="index" :label="item.name" :value="item.code" />
							</el-select>
						</el-form-item>
					</el-col>
					<template v-if="state.ruleForm.type == 'length' && (state.column.netType.includes('int') || state.column.netType.includes('long') || state.column.netType.includes('string'))">
						<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
							<el-form-item label="最小值" prop="min" :rules="rules.min">
								<el-input-number v-model="state.ruleForm.min" :min="0" :max="100000" />
							</el-form-item>
						</el-col>
						<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
							<el-form-item label="最大值" prop="max" :rules="rules.max">
								<el-input-number v-model="state.ruleForm.max" :min="0" :max="100000" />
							</el-form-item>
						</el-col>
					</template>
					<template v-if="state.ruleForm.type == 'length' && (state.column.netType.includes('decimal') || state.column.netType.includes('float') || state.column.netType.includes('double'))">
						<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
							<el-form-item label="最小值" prop="min" :rules="rules.minDecimal">
								<el-input-number v-model="state.ruleForm.min" :min="0" :max="100000" />
							</el-form-item>
						</el-col>
						<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
							<el-form-item label="最大值" prop="max" :rules="rules.maxDecimal">
								<el-input-number v-model="state.ruleForm.max" :min="0" :max="100000" />
							</el-form-item>
						</el-col>
					</template>
					<template v-if="state.ruleForm.type == 'length' && state.column.netType.includes('DateTime')">
						<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
							<el-form-item label="起始日期" prop="min" :rules="rules.minDate">
								<el-date-picker v-model="state.ruleForm.min" type="date" format="YYYY-MM-DD" value-format="YYYY-MM-DD" placeholder="请选择起始日期" />
							</el-form-item>
						</el-col>
						<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
							<el-form-item label="结束日期" prop="max" :rules="rules.maxDate">
								<el-date-picker v-model="state.ruleForm.max" type="date" format="YYYY-MM-DD" value-format="YYYY-MM-DD" placeholder="请选择结束日期" />
							</el-form-item>
						</el-col>
					</template>
					<template v-if="state.ruleForm.type == 'pattern'">
						<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
							<el-form-item label="提示信息" prop="message" :rules="rules.message">
								<el-input v-model="state.ruleForm.message" placeholder="请输入提示信息" maxlength="128" show-word-limit clearable />
							</el-form-item>
						</el-col>
						<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
							<el-form-item label="正则式" prop="pattern" :rules="rules.pattern">
								<el-input v-model="state.ruleForm.pattern" placeholder="请输入正则表达式">
									<template #append>
										<el-button @click="openPatternDialog">选择正则</el-button>
									</template>
								</el-input>
							</el-form-item>
						</el-col>
					</template>
				</el-row>
			</el-form>
			<template #footer>
				<span class="dialog-footer">
					<el-button icon="ele-CircleCloseFilled" @click="cancel">取 消</el-button>
					<el-button type="primary" icon="ele-CircleCheckFilled" @click="submit">确 定</el-button>
				</span>
			</template>
		</el-dialog>

		<patternDialog ref="patternDialogRef" @submit-pattern="submitPatternOK" />
	</div>
</template>

<style lang="scss" scoped>
.tool-box {
	padding-bottom: 20px;
	display: flex;
	gap: 20px;
	align-items: center;
	// background: red;
}
</style>

<script lang="ts" setup>
import { reactive, ref, toRaw } from 'vue';
import patternDialog from '/@/views/system/codeGen/component/patternDialog.vue';
import { ElMessage } from 'element-plus';
import type { FormRules } from 'element-plus';

const emit = defineEmits(['submitRule']);
// 自行添加其他规则
const rules = ref<FormRules>({
	type: [
		{
			required: true,
			message: '请选择验证类型',
			trigger: 'change',
		},
	],
	min: [
		{
			type: 'integer',
			required: true,
			pattern: /[^\d]/g,
			message: '请输入正确的数字',
			trigger: 'blur',
		},
	],
	minDecimal: [
		{
			required: true,
			message: '请输入正确的数字',
			trigger: 'blur',
		},
	],
	minDate: [
		{
			required: true,
			pattern: /^\d{4}-\d{2}-\d{2}$/,
			message: '请选择起始日期',
			trigger: ['change', 'blur'],
		},
	],
	max: [
		{
			type: 'integer',
			required: true,
			pattern: /[^\d]/g,
			message: '请输入正确的数字',
			trigger: 'blur',
		},
	],
	maxDecimal: [
		{
			required: true,
			message: '请输入正确的数字',
			trigger: 'blur',
		},
	],
	maxDate: [
		{
			required: true,
			pattern: /^\d{4}-\d{2}-\d{2}$/,
			message: '请选择结束日期',
			trigger: ['change', 'blur'],
		},
	],
	message: [
		{
			required: true,
			message: '请输入提示信息',
			trigger: 'blur',
		},
	],
	pattern: [{ required: true, message: '请输入正则表达式', trigger: 'blur' }],
});
const ruleFormRef = ref();
const patternDialogRef = ref();
const validTypeData = ref([
	{ code: 'required', name: '必填验证' },
	{ code: 'remote', name: '远程验证' },
	{ code: 'array', name: '数组验证' },
	{ code: 'pattern', name: '正则模式' },
	{ code: 'length', name: '长度限制' },
]);

const state = reactive({
	isShowDialog: false,
	loading: false,
	ruleForm: {} as any,
	id: 0,
	column: {} as any,
});

// 打开弹窗
const openDialog = (row: any) => {
	// const data = JSON.parse(JSON.stringify(row));
	// state.ruleForm = data;
	state.id = row.id;
	state.isShowDialog = true;
	state.column = row;
};

// 关闭弹窗
const closeDialog = () => {
	// emit("reloadTable");
	ruleFormRef.value.resetFields();
	state.isShowDialog = false;
};

// 打开正则选择框
const openPatternDialog = () => {
	patternDialogRef.value.openDialog();
};

// 取消
const cancel = () => {
	// state.isShowDialog = false;
	closeDialog();
};

const submitPatternOK = (data: any) => {
	// console.log('submitPatternOK', data);
	state.ruleForm.pattern = data.pattern;
};

// 提交
const submit = async () => {
	ruleFormRef.value.validate(async (isValid: boolean, fields?: any) => {
		if (isValid) {
			let values = toRaw(state.ruleForm);
			values.key = toRaw(state.id);
			emit('submitRule', Object.assign({}, values));
			closeDialog();
		} else {
			ElMessage({
				message: `表单有${Object.keys(fields).length}处验证失败，请修改后再提交`,
				type: 'error',
			});
		}
	});
};

// 类型发生改变
const handleTypeChange = () => {
	resetFields();
};
// 重置表单值
const resetFields = () => {
	state.ruleForm.message = '';
	state.ruleForm.min = 0;
	state.ruleForm.max = 10;
	state.ruleForm.pattern = '';
	// state.ruleForm.dataType=null;
};

// 导出对象
defineExpose({ openDialog });
</script>
