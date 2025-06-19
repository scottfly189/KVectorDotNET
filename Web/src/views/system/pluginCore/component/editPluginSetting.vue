<template>
	<div class="pluginCore-container">
		<el-dialog v-model="state.isShowDialog" draggable :close-on-click-modal="false" width="900px">
			<template #header>
				<div style="color: #fff">
					<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-Edit /> </el-icon>
					<span> {{ props.title }} </span>
				</div>
			</template>
			<el-tabs>
				<el-tab-pane label="Json代码">
					<div ref="monacoEditorRef" style="width: 100%; height: 500px"></div>
				</el-tab-pane>
			</el-tabs>
			<template #footer>
				<span class="dialog-footer">
					<el-button @click="cancel">取 消</el-button>
					<el-button type="primary" @click="submit">确 定</el-button>
				</span>
			</template>
		</el-dialog>
	</div>
</template>

<script lang="ts" setup name="sysEditPluginCoreSetting">
import { reactive, ref } from 'vue';
import { ElMessage } from 'element-plus';
import * as monaco from 'monaco-editor';

import { getAPI } from '/@/utils/axios-utils';
import { SysPluginCoreApi } from '/@/api-plugins/pluginCore/api';
import { UpdatePluginCoreSettingInput } from '/@/api-plugins/pluginCore/models';

const props = defineProps({
	title: String,
});
const emits = defineEmits(['handleQuery']);
const ruleFormRef = ref();
const monacoEditorRef = ref();
const state = reactive({
	isShowDialog: false,
	ruleForm: {} as UpdatePluginCoreSettingInput,
	selectedTabName: '0', // 选中的 tab
});

// 初始化monacoEditor对象
var monacoEditor: any = null;
const initMonacoEditor = () => {
	monacoEditor = monaco.editor.create(monacoEditorRef.value, {
		theme: 'vs-dark', // 主题 vs vs-dark hc-black
		value: state.ruleForm.data, // 默认显示的值
		language: 'csharp',
		formatOnPaste: true,
		wordWrap: 'on', //自动换行，注意大小写
		wrappingIndent: 'indent',
		folding: true, // 是否折叠
		foldingHighlight: true, // 折叠等高线
		foldingStrategy: 'indentation', // 折叠方式  auto | indentation
		showFoldingControls: 'always', // 是否一直显示折叠 always | mouSEOver
		disableLayerHinting: true, // 等宽优化
		emptySelectionClipboard: false, // 空选择剪切板
		selectionClipboard: false, // 选择剪切板
		automaticLayout: true, // 自动布局
		codeLens: false, // 代码镜头
		scrollBeyondLastLine: false, // 滚动完最后一行后再滚动一屏幕
		colorDecorators: true, // 颜色装饰器
		accessibilitySupport: 'auto', // 辅助功能支持  "auto" | "off" | "on"
		lineNumbers: 'on', // 行号 取值： "on" | "off" | "relative" | "interval" | function
		lineNumbersMinChars: 5, // 行号最小字符   number
		//enableSplitViewResizing: false,
		readOnly: false, //是否只读  取值 true | false
	});
};

// 打开弹窗
const openDialog = async (row: any) => {
	state.isShowDialog = true;

	var res = await getAPI(SysPluginCoreApi).apiSysPluginCoreSettingGet(row.id);
	state.ruleForm.data = res.data.result;
	state.ruleForm.id = row.id;
	// 延迟取值防止取不到
	setTimeout(() => {
		if (monacoEditor == null) initMonacoEditor();
	}, 1);
};

// 关闭弹窗
const closeDialog = () => {
	emits('handleQuery');
	state.isShowDialog = false;
};

// 取消
const cancel = () => {
	state.isShowDialog = false;
};

// 提交
const submit = async () => {
	state.ruleForm.data = monacoEditor.getValue();
	if (state.ruleForm.data.length < 10) {
		ElMessage.warning('请正确编写 Json 代码');
		return;
	}
	await getAPI(SysPluginCoreApi).apiSysPluginCoreSettingUpdatePost(state.ruleForm);

	closeDialog();
};

// 导出对象
defineExpose({ openDialog });
</script>
