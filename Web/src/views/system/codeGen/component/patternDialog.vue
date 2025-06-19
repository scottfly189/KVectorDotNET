<template>
	<div class="sys-codeGenConfig-container">
		<el-dialog v-model="state.isShowDialog" draggable :close-on-click-modal="false" width="700px">
			<template #header>
				<div style="color: #fff">
					<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-Edit /> </el-icon>
					<span> 选择正则 </span>
				</div>
			</template>
			<div class="pattern-box">
				<div class="pattern-item" v-for="(item, index) in patternData" :key="index" :class="{ active: state.index == index }" @click="handlePatternClick(item, index)">
					<div class="title">{{ item.title }}</div>
					<div class="info">{{ item.info }}</div>
					<div class="pattern">{{ item.pattern }}</div>
				</div>
			</div>
			<template #footer>
				<span class="dialog-footer">
					<el-button icon="ele-CircleCloseFilled" @click="cancel">取 消</el-button>
					<el-button type="primary" icon="ele-CircleCheckFilled" @click="submit">确 定</el-button>
				</span>
			</template>
		</el-dialog>
	</div>
</template>

<script lang="ts" setup>
import { reactive, ref } from 'vue';
import { ElMessage } from 'element-plus';

const patternData = ref([
	{
		title: '手机号',
		info: '',
		pattern: '/^1[3,4,5,6,7,8,9][0-9]{9}$/',
	},
	{
		title: '邮箱',
		info: '',
		pattern: '/^([a-zA-Z]|[0-9])(\\w|\\-)+@[a-zA-Z0-9]+\\.([a-zA-Z]{2,4})$/',
	},
	{
		title: '网址',
		info: '',
		pattern: '/^http(s)?://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?$/',
	},
	{
		title: '字母/数字/下划线',
		info: '',
		pattern: '/^\\w+$/',
	},
	{
		title: '中英文/数字/下划线',
		info: '',
		pattern: '/^[\\u4e00-\\u9fa5_a-zA-Z0-9]+$/',
	},
	{
		title: '中文/英文',
		info: '',
		pattern: '/^[\\u4e00-\\u9fa5a-zA-Z]+$/',
	},
	{
		title: '规范金额',
		info: '',
		pattern: '/(^[\\d]|^[1-9][\\d]*)($|[\\.][\\d]{0,2}$)/',
	},
	{
		title: '用户名不能全是数字',
		info: '',
		pattern: '/[^\\d]/g',
	},
	{
		title: '中文',
		info: '',
		pattern: '/^[\\u4e00-\\u9fa5]+$/',
	},
	{
		title: '非中文',
		info: '',
		pattern: '/^[^\\u4e00-\\u9fa5]*$/',
	},
	{
		title: '限制长度',
		info: '',
		pattern: '/^\\d{1,20}$/',
	},
	{
		title: '数字',
		info: '',
		pattern: '/^[0-9]*$/',
	},
	{
		title: '正整数及整数',
		info: '',
		pattern: '/^[1-9]\\d*$/',
	},
	{
		title: '数字(可正负)',
		info: '',
		pattern: '/^-[0-9]*[1-9][0-9]*$/',
	},
	{
		title: '数字/小数点',
		info: '',
		pattern: '/^\\d+$|^\\d*\\.\\d+$/',
	},
	{
		title: '合法IP地址',
		info: '',
		pattern: '/^(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])$/',
	},
	{
		title: '手机号码或者固话',
		info: '',
		pattern: '/^((0\\d{2,3}-\\d{7,8})|(1[3456789]\\d{9}))$/',
	},
	{
		title: '身份证号码',
		info: '',
		pattern: '/(^\\d{15}$)|(^\\d{18}$)|(^\\d{17}(\\d|X|x)$)/',
	},
	{
		title: '大写字母',
		info: '',
		pattern: '/^[A-Z]+$/',
	},
	{
		title: '小写字母',
		info: '',
		pattern: '/^[a-z]+$/',
	},
	{
		title: '大小写混合',
		info: '',
		pattern: '/^[A-Za-z]+$/',
	},
	{
		title: '多个8位数字格式(yyyyMMdd)并以逗号隔开',
		info: '',
		pattern: '/^\\d{8}(\\,\\d{8})*$/',
	},
	{
		title: '数字加英文',
		info: '',
		pattern: '/^[a-zA-Z0-9]+$/',
	},
	{
		title: '前两位是数字后一位是英文',
		info: '',
		pattern: '/^\\d{2}[a-zA-Z]+$/',
	},
	{
		title: '1到100的数字',
		info: '',
		pattern: '/^[0-9]\\d{0,1}$/',
	},
	{
		title: '1-1000两位小数',
		info: '',
		pattern: '/^(.*[^0-9]|)(1000|[1-9]\\d{0,2})([^0-9].*|)$/',
	},
	{
		title: '小数点后只能有两位数(可为0)',
		info: '',
		pattern: '/^(-?\\d+)(\\.\\d{1,2})?$/',
	},
	{
		title: '密码正则',
		info: '以字母开头，长度在6~18之间，只能包含字母、数字和下划线',
		pattern: '/^[a-zA-Z]\\w{5,17}$/',
	},
	{
		title: '强密码',
		info: '必须包含大小写字母和数字的组合，不能使用特殊字符，长度在8-10之间',
		pattern: '/^(?=.\\d)(?=.[a-z])(?=.[A-Z]).{8,10}$/',
	},
	{
		title: '强密码',
		info: '最少6位，包括至少1个大写字母，1个小写字母，1个数字，1个特殊字符',
		pattern: '/^.*(?=.{6,})(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*? ]).*$/',
	},
	{
		title: 'QQ号码',
		info: '',
		pattern: '/^[1-9][0-9]{4,12}$/',
	},
	{
		title: '微信号码',
		info: '',
		pattern: '/^[a-zA-Z]([-_a-zA-Z0-9]{5,19})+$/',
	},
	{
		title: '域名',
		info: '',
		pattern: '/^[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(/.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+/.?$/',
	},
	{
		title: '车牌号码',
		info: '',
		pattern: '/^[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z]{1}[A-Z0-9]{4}[A-Z0-9挂学警港澳]{1}$/',
	},
	{
		title: '护照',
		info: '',
		pattern: '/^(P\\d{7}|G\\d{7,8}|TH\\d{7,8}|S\\d{7,8}|A\\d{7,8}|L\\d{7,8}|\\d{9}|D\\d+|1[4,5]\\d{7})$/',
	},
	{
		title: '固定电话',
		info: '',
		pattern: '/^(\\(\\d{3,4}\\)|\\d{3,4}-|\\s)?\\d{8}$/',
	},
	{
		title: '邮政编码',
		info: '',
		pattern: '/^[1-9]{1}(\\d+){5}$/',
	},
	{
		title: '经度',
		info: '',
		pattern: '/^(\\-|\\+)?(((\\d|[1-9]\\d|1[0-7]\\d|0{1,3})\\.\\d{0,6})|(\\d|[1-9]\\d|1[0-7]\\d|0{1,3})|180\\.0{0,6}|180)$/',
	},
	{
		title: '纬度',
		info: '',
		pattern: '/^(\\-|\\+)?([0-8]?\\d{1}\\.\\d{0,6}|90\\.0{0,6}|[0-8]?\\d{1}|90)$/',
	},
	{
		title: '正整数 + 0',
		info: '',
		pattern: '/^\\d+$/',
	},
	{
		title: '正整数',
		info: '',
		pattern: '/^[0-9]*[1-9][0-9]*$/',
	},
	{
		title: '负整数 + 0',
		info: '',
		pattern: '/^((-\\d+)|(0+))$/',
	},
	{
		title: '负整数',
		info: '',
		pattern: '/^-[0-9]*[1-9][0-9]*$/',
	},
	{
		title: '匹配整数',
		info: '',
		pattern: '/^-?\\d+$/',
	},
]);
const state = reactive({
	isShowDialog: false,
	loading: false,
	selectPattern: null,
	index: -1,
});

const emit = defineEmits(['submitPattern']);

// 打开弹窗
const openDialog = () => {
	state.isShowDialog = true;
};

// 关闭弹窗
const closeDialog = () => {
	state.isShowDialog = false;
};

// 取消
const cancel = () => {
	closeDialog();
};

const handlePatternClick = (item: any, index: number) => {
	state.selectPattern = item;
	state.index = index;
	ElMessage({
		message: `点击确定完成操作！`,
		type: 'success',
	});
};

// 提交
const submit = async () => {
	emit('submitPattern', state.selectPattern);
	closeDialog();
};

// 导出对象
defineExpose({ openDialog });
</script>

<style lang="scss" scoped>
.pattern-box {
	display: flex;
	justify-content: space-between;
	flex-wrap: wrap;
	height: 400px;
	overflow-y: auto;
	padding: 0px 10px;

	.pattern-item {
		width: 48%;
		border: 1px solid #d9d9d9;
		box-sizing: border-box;
		padding: 10px;
		border-radius: 5px;
		cursor: pointer;
		flex-shrink: 0;
		margin: 5px 0px;

		.title {
			font-weight: 700;
		}

		.info {
			font-size: 12px;
			margin-top: 5px;
		}

		.pattern {
			font-size: 10px;
			margin-top: 5px;
			background: #1d1f21;
			padding: 3px 5px;
			color: #d9d9d9;
			border-radius: 3px;
		}

		&:hover {
			//border: 1px solid red;
			box-shadow: 0px 0px 2px 1px rgba(0, 0, 0, 0.2);
		}
	}
}

.active {
	border: 1px solid red !important;
}
</style>
