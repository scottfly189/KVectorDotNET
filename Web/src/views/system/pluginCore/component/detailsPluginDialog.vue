<template>
	<div class="pluginCore-container">
		<el-dialog v-model="state.isShowDialog" draggable :close-on-click-modal="false" width="700px">
			<template #header>
				<div style="color: #fff">
					<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-Edit /> </el-icon>
					<span> {{ props.title }} </span>
				</div>
			</template>
			<el-form :model="state.ruleForm" label-width="auto">
				<el-row :gutter="8" style="width: 100%">
					<el-col :span="24" :xs="24">
						<el-card shadow="hover">
							<div class="account-center-org">
								<p>
									<MdPreview id="preview-only" v-model="state.ruleForm.content" theme="dark" />
								</p>
							</div>
						</el-card>
					</el-col>
				</el-row>
			</el-form>
		</el-dialog>
	</div>
</template>

<script lang="ts" setup name="sysPluginCoreDetail">
import { onMounted, reactive } from 'vue';
// preview.css相比style.css少了编辑器那部分样式
import 'md-editor-v3/lib/preview.css';
import { MdPreview, MdCatalog, MdEditor } from 'md-editor-v3';

import { getAPI } from '/@/utils/axios-utils';
import { SysPluginCoreApi } from '/@/api-plugins/pluginCore/api';

const props = defineProps({
	title: String,
});

const state = reactive({
	loading: false,
	isShowDialog: false,
	ruleForm: [] as any,
});

onMounted(async () => {});

// 打开弹窗
const openDialog = async (row: any) => {
	state.isShowDialog = true;
	state.loading = true;
	var res = await getAPI(SysPluginCoreApi).apiSysPluginCoreReadmeGet(row.id);
	state.ruleForm = res.data.result;
	state.loading = false;
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
const submit = () => {
	closeDialog();
};

// 导出对象
defineExpose({ openDialog });
</script>
