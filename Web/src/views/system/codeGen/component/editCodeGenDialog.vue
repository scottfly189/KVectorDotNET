<template>
	<div class="sys-editCodeGen-container">
		<el-dialog v-model="state.isShowDialog" draggable :close-on-click-modal="false" width="980px">
			<template #header>
				<div style="color: #fff">
					<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-Edit /> </el-icon>
					<span> {{ props.title }} </span>
				</div>
			</template>
			<el-tabs v-model="activeTab" class="demo-tabs">
				<el-tab-pane label="代码生成" name="codeGen" style="height: 700px">
					<div style="color: red; padding: 10px 10px; background: #faecd8; margin-bottom: 10px">
						<el-icon style="transform: translateY(2px)"><ele-Bell /></el-icon>
						<span> 若找不到在前端生成的实体/表，请检查配置文件中实体所在程序集或重启后台服务。 </span>
					</div>
					<el-form :model="state.ruleForm" ref="ruleFormRef" label-width="auto">
						<el-row :gutter="10">
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
								<el-form-item label="库定位器" prop="configId" :rules="[{ required: true, message: '请选择库定位器', trigger: 'blur' }]">
									<el-select v-model="state.ruleForm.configId" placeholder="库名" filterable @change="dbChanged()" class="w100">
										<el-option v-for="item in state.dbData" :key="item.configId" :label="item.configId" :value="item.configId" />
									</el-select>
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
								<el-form-item label="库类型" prop="dbType" :rules="[{ required: true, message: '描述不能为空', trigger: 'blur' }]">
									<el-select v-model="state.ruleForm.dbType" placeholder="数据库类型" filterable clearable disabled class="w100">
										<el-option label="MySql" :value="'0'" />
										<el-option label="SqlServer" :value="'1'" />
										<el-option label="Sqlite" :value="'2'" />
										<el-option label="Oracle" :value="'3'" />
										<el-option label="PostgreSQL" :value="'4'" />
										<el-option label="Dm" :value="'5'" />
										<el-option label="Kdbndp" :value="'6'" />
										<el-option label="Oscar" :value="'7'" />
										<el-option label="MySqlConnector" :value="'8'" />
										<el-option label="Access" :value="'9'" />
										<el-option label="OpenGauss" :value="'10'" />
										<el-option label="QuestDB" :value="'11'" />
										<el-option label="HG" :value="'12'" />
										<el-option label="ClickHouse" :value="'13'" />
										<el-option label="GBase" :value="'14'" />
										<el-option label="Odbc" :value="'15'" />
										<el-option label="OceanBaseForOracle" :value="'16'" />
										<el-option label="TDengine" :value="'17'" />
										<el-option label="GaussDB" :value="'18'" />
										<el-option label="OceanBase" :value="'19'" />
										<el-option label="Tidb" :value="'20'" />
										<el-option label="Vastbase" :value="'21'" />
										<el-option label="PolarDB" :value="'22'" />
										<el-option label="Doris" :value="'23'" />
										<el-option label="Custom" :value="'900'" />
									</el-select>
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
								<el-form-item label="库地址" prop="connectionString" :rules="[{ required: true, message: '库地址不能为空', trigger: 'blur' }]">
									<el-input v-model="state.ruleForm.connectionString" disabled clearable type="textarea" />
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
								<el-form-item label="生成表" prop="tableName" :rules="[{ required: true, message: '生成表不能为空', trigger: 'blur' }]">
									<template v-slot:label>
										<div>
											生成表
											<el-tooltip raw-content content="若找不到在前端生成的实体/表，请检查配置文件中实体所在程序集或重启后台服务。" placement="top">
												<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"><ele-QuestionFilled /></el-icon>
											</el-tooltip>
										</div>
									</template>
									<el-select v-model="state.ruleForm.tableName" @change="tableChanged" value-key="value" filterable clearable class="w100">
										<el-option v-for="item in state.tableData" :key="item.entityName" :label="item.entityName + ' ( ' + item.tableName + ' ) [' + item.tableComment + ']'" :value="item" />
									</el-select>
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
								<el-form-item label="表类型" prop="busName" :rules="[{ required: true, message: '表类型不能为空', trigger: 'blur' }]">
									<g-sys-dict v-model="state.ruleForm.tabType" code="code_gen_tab_type" render-as="select" />
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
								<el-form-item label="业务名" prop="busName" :rules="[{ required: true, message: '业务名不能为空', trigger: 'blur' }]">
									<el-input v-model="state.ruleForm.busName" placeholder="请输入" clearable />
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
								<el-form-item label="Name字段" prop="treeName">
									<template v-slot:label>
										<div>
											Name字段
											<el-tooltip raw-content content="本表作为树控件的树属性Name字段。" placement="top">
												<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"><ele-QuestionFilled /></el-icon>
											</el-tooltip>
										</div>
									</template>
									<el-select v-model="state.ruleForm.treeName" @change="treeNameChanged" value-key="value" filterable clearable class="w100">
										<el-option v-for="item in state.columnData" :key="item.columnName" :label="item.columnName + ' ( ' + item.columnName + ' ) [' + item.columnComment + ']'" :value="item" />
									</el-select>
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
								<el-form-item label="树key字段" prop="treeKey">
									<template v-slot:label>
										<div>
											PidKey字段
											<el-tooltip raw-content content="本表作为树控件的树属性父ID，PidKey字段字段，没有就请留空。" placement="top">
												<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"><ele-QuestionFilled /></el-icon>
											</el-tooltip>
										</div>
									</template>
									<el-select v-model="state.ruleForm.treeKey" @change="treeKeyChanged" value-key="value" filterable clearable class="w100">
										<el-option v-for="item in state.columnData" :key="item.columnName" :label="item.columnName + ' ( ' + item.columnName + ' ) [' + item.columnComment + ']'" :value="item" />
									</el-select>
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
								<el-form-item label="生成菜单" prop="generateMenu">
									<el-radio-group v-model="state.ruleForm.generateMenu">
										<el-radio :value="true">是</el-radio>
										<el-radio :value="false">否</el-radio>
									</el-radio-group>
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
								<el-form-item label="菜单图标" prop="menuIcon">
									<IconSelector v-model="state.ruleForm.menuIcon" :size="getGlobalComponentSize" placeholder="菜单图标" type="all" />
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
								<el-form-item label="父级菜单" prop="menuPid">
									<el-cascader
										:options="state.menuData"
										:props="cascaderProps"
										placeholder="请选择上级菜单"
										:disabled="!state.ruleForm.generateMenu"
										filterable
										clearable
										class="w100"
										v-model="state.ruleForm.menuPid"
										@change="menuChange"
									>
										<template #default="{ node, data }">
											<span>{{ data.title }}</span>
											<span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
										</template>
									</el-cascader>
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
								<el-form-item label="命名空间" prop="nameSpace" :rules="[{ required: true, message: '请选择命名空间', trigger: 'blur' }]">
									<!-- <el-input v-model="state.ruleForm.nameSpace" clearable placeholder="请输入" /> -->
									<el-select v-model="state.ruleForm.nameSpace" filterable clearable class="w100" placeholder="命名空间">
										<el-option v-for="(item, index) in props.applicationNamespaces" :key="index" :label="item" :value="item" />
									</el-select>
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
								<el-form-item label="前端目录" prop="pagePath" :rules="[{ required: true, message: '前端目录不能为空', trigger: 'blur' }]">
									<el-input v-model="state.ruleForm.pagePath" clearable placeholder="请输入" />
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
								<el-form-item label="作者姓名" prop="authorName">
									<el-input v-model="state.ruleForm.authorName" clearable placeholder="请输入" />
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
								<el-form-item label="生成方式" prop="generateType">
									<g-sys-dict v-model="state.ruleForm.generateType" code="code_gen_create_type" render-as="select" />
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
								<el-form-item label="接口模式" prop="isApiService">
									<template v-slot:label>
										<div>
											接口模式
											<el-tooltip raw-content content="接口服务模式是指根据swagger自动生成前端接口请求文件，推荐此模式。传统模式则是指手动编写接口请求并进行数据绑定。" placement="top">
												<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"><ele-QuestionFilled /></el-icon>
											</el-tooltip>
										</div>
									</template>
									<el-radio-group v-model="state.ruleForm.isApiService">
										<el-radio :value="true">接口服务</el-radio>
										<el-radio :value="false">传统模式</el-radio>
									</el-radio-group>
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
								<el-form-item label="支持打印" prop="printType">
									<g-sys-dict v-model="state.ruleForm.printType" code="code_gen_print_type" render-as="select" />
								</el-form-item>
							</el-col>
							<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20" v-if="state.ruleForm.printType == 'custom'">
								<el-form-item label="打印模版" prop="printName">
									<el-select v-model="state.ruleForm.printName" filterable class="w100">
										<el-option v-for="item in state.printList" :key="item.id" :label="item.name" :value="item.name" />
									</el-select>
								</el-form-item>
							</el-col>

							<!-- <el-divider border-style="dashed" content-position="center">
		<div style="color: #b1b3b8">数据唯一性配置</div>
	</el-divider>
	<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
		<el-button icon="ele-Plus" type="primary" plain @click="() => state.ruleForm.tableUniqueList?.push({})"> 增加配置 </el-button>
		<span style="font-size: 12px; color: gray; padding-left: 5px"> 保证字段值的唯一性，排除null值 </span>
	</el-col>
	<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
		<template v-if="state.ruleForm.tableUniqueList != undefined && state.ruleForm.tableUniqueList.length > 0">
			<el-row :gutter="10" v-for="(v, k) in state.ruleForm.tableUniqueList" :key="k">
				<el-col :xs="24" :sm="14" :md="14" :lg="14" :xl="14" class="mb20">
					<el-form-item label="字段" :prop="`tableUniqueList[${k}].columns`" :rules="[{ required: true, message: `字段不能为空`, trigger: 'blur' }]">
						<template #label>
							<el-button icon="ele-Delete" type="danger" circle plain size="small" @click="() => state.ruleForm.tableUniqueList?.splice(k, 1)" />
							<span class="ml5">字段</span>
						</template>
						<el-select
							v-model="state.ruleForm.tableUniqueList[k].columns"
							@change="(val: any) => changeTableUniqueColumn(val, k)"
							multiple
							filterable
							clearable
							collapse-tags
							collapse-tags-tooltip
							class="w100"
						>
							<el-option v-for="item in state.columnData" :key="item.columnName" :label="item.columnName + ' [' + item.columnComment + ']'" :value="item.columnName" />
						</el-select>
					</el-form-item>
				</el-col>
				<el-col :xs="24" :sm="10" :md="10" :lg="10" :xl="10" class="mb20">
					<el-form-item label="描述信息" :prop="`tableUniqueList[${k}].message`" :rules="[{ required: true, message: `描述信息不能为空`, trigger: 'blur' }]">
						<el-input v-model="state.ruleForm.tableUniqueList[k].message" clearable placeholder="请输入" />
					</el-form-item>
				</el-col>
			</el-row>
		</template>
	</el-col> -->
							<el-col>
								<el-divider content-position="center"> 左边布局显示树形列表；右边布局上下结构显示主子表数据列表 </el-divider>

								<!-- <p><el-tag style="border: 1 solid var(--el-border-color)">以下默认页面左右布局，左边布局显示树形列表，右边布局上下结构显示主子表数据列表</el-tag></p> -->
								<el-tabs v-model="activeName" class="demo-tabs">
									<el-tab-pane label="页面左（树列表）" name="1">
										<el-row :gutter="10">
											<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
												<el-form-item label="树 - 库定位器" prop="configId2">
													<el-select v-model="state.ruleForm.configId2" placeholder="库名" filterable @change="dbChanged2()" class="w100">
														<el-option v-for="item in state.dbData" :key="item.configId" :label="item.configId" :value="item.configId" />
													</el-select>
												</el-form-item>
											</el-col>
											<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
												<el-form-item label="树表名称">
													<template v-slot:label>
														<div>
															树表名称
															<el-tooltip raw-content content="若找不到在前端生成的实体/表，同上，如表有下划线_则因实体去掉划线取不到字段。" placement="top">
																<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"><ele-QuestionFilled /></el-icon>
															</el-tooltip>
														</div>
													</template>
													<el-select v-model="state.ruleForm.leftTab" @change="leftTableChanged" value-key="value" filterable clearable class="w100">
														<el-option v-for="item in state.tableData2" :key="item.entityName" :label="item.entityName + ' ( ' + item.tableName + ' ) [' + item.tableComment + ']'" :value="item" />
													</el-select>
												</el-form-item>
											</el-col>

											<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
												<el-form-item label="树关联字段">
													<el-select v-model="state.ruleForm.leftKey" @change="leftKeyChanged" value-key="value" filterable clearable class="w100">
														<el-option v-for="item in state.lcolumnData" :key="item.columnName" :label="item.columnName + ' ( ' + item.columnName + ' ) [' + item.columnComment + ']'" :value="item" />
													</el-select>
												</el-form-item>
											</el-col>
											<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
												<el-form-item label="关联主表字段">
													<template v-slot:label>
														<div>
															关联主表字段
															<el-tooltip raw-content content="先选择主表才可以选择字段。" placement="top">
																<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"><ele-QuestionFilled /></el-icon>
															</el-tooltip>
														</div>
													</template>
													<el-select v-model="state.ruleForm.leftPrimaryKey" @change="leftPrimaryKeyChanged" value-key="value" filterable clearable class="w100">
														<el-option v-for="item in state.columnData" :key="item.columnName" :label="item.columnName + ' ( ' + item.columnName + ' ) [' + item.columnComment + ']'" :value="item" />
													</el-select>
												</el-form-item>
											</el-col>

											<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
												<el-form-item label="树显示名称">
													<el-select v-model="state.ruleForm.leftName" @change="leftNameChanged" value-key="value" filterable clearable class="w100">
														<el-option v-for="item in state.lcolumnData" :key="item.columnName" :label="item.columnName + ' ( ' + item.columnName + ' ) [' + item.columnComment + ']'" :value="item" />
													</el-select>
												</el-form-item>
											</el-col>

											<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
												<el-form-item label="模板">
													<el-input v-model="state.ruleForm.template" clearable placeholder="请输入" />
												</el-form-item>
											</el-col>
										</el-row>
									</el-tab-pane>
									<el-tab-pane label="页面右（主子表）" name="2">
										<el-row :gutter="10">
											<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
												<el-form-item label="子表 - 库定位器" prop="configId3">
													<el-select v-model="state.ruleForm.configId3" placeholder="库名" filterable @change="dbChanged3()" class="w100">
														<el-option v-for="item in state.dbData" :key="item.configId" :label="item.configId" :value="item.configId" />
													</el-select>
												</el-form-item>
											</el-col>
											<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
												<el-form-item label="子表名称" prop="bottomTab">
													<template v-slot:label>
														<div>
															子表名称
															<el-tooltip raw-content content="若找不到在前端生成的实体/表，同上，如表有下划线_则因实体去掉划线取不到字段。" placement="top">
																<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"><ele-QuestionFilled /></el-icon>
															</el-tooltip>
														</div>
													</template>
													<el-select v-model="state.ruleForm.bottomTab" @change="bottomTableChanged" value-key="value" filterable clearable class="w100">
														<el-option v-for="item in state.tableData3" :key="item.entityName" :label="item.entityName + ' ( ' + item.tableName + ' ) [' + item.tableComment + ']'" :value="item" />
													</el-select>
												</el-form-item>
											</el-col>
											<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
												<el-form-item label="子表关联字段" prop="bottomKey">
													<el-select v-model="state.ruleForm.bottomKey" @change="bottomKeyChanged" value-key="value" filterable clearable class="w100">
														<el-option v-for="item in state.bcolumnData" :key="item.columnName" :label="item.columnName + ' ( ' + item.columnName + ' ) [' + item.columnComment + ']'" :value="item" />
													</el-select>
												</el-form-item>
											</el-col>
											<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
												<el-form-item label="关联主表字段" prop="bottomPrimaryKey">
													<template v-slot:label>
														<div>
															关联主表字段
															<el-tooltip raw-content content="先选择主表才可以选择字段。" placement="top">
																<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"><ele-QuestionFilled /></el-icon>
															</el-tooltip>
														</div>
													</template>
													<el-select v-model="state.ruleForm.bottomPrimaryKey" @change="bottomPrimaryKeyChanged" value-key="value" filterable clearable class="w100">
														<el-option v-for="item in state.columnData" :key="item.columnName" :label="item.columnName + ' ( ' + item.columnName + ' ) [' + item.columnComment + ']'" :value="item" />
													</el-select>
												</el-form-item>
											</el-col>
										</el-row>
									</el-tab-pane>
								</el-tabs>
							</el-col>
						</el-row>
					</el-form>
				</el-tab-pane>
				<el-tab-pane label="选择模板" name="template" style="height: 700px">
					<el-table ref="templateTableRef" :data="templateTableData" @selection-change="handleSelectionChange" style="width: 100%">
						<el-table-column type="selection" width="55" />
						<el-table-column property="name" label="模板文件名" width="280" />
						<el-table-column property="describe" label="描述" show-overflow-tooltip />
					</el-table>
				</el-tab-pane>
			</el-tabs>
			<template #footer>
				<span class="dialog-footer">
					<el-button icon="ele-CircleCloseFilled" @click="cancel">取 消</el-button>
					<el-button type="primary" icon="ele-CircleCheckFilled" @click="submit">确 定</el-button>
				</span>
			</template>
		</el-dialog>
	</div>
</template>

<script lang="ts" setup name="sysEditCodeGen">
import { computed, onMounted, reactive, ref, nextTick } from 'vue';
import IconSelector from '/@/components/iconSelector/index.vue';
import { ElMessage } from 'element-plus';
import other from '/@/utils/other';
import { useUserInfo } from '/@/stores/userInfo';

import { getAPI } from '/@/utils/axios-utils';
import { SysCodeGenApi, SysMenuApi, SysPrintApi, SysCodeGenTemplateApi } from '/@/api-services/api';
import { UpdateCodeGenInput, AddCodeGenInput, SysMenu, SysPrint } from '/@/api-services/models';

const props = defineProps({
	title: String,
	applicationNamespaces: Array<String>,
});
const emits = defineEmits(['handleQuery']);
const ruleFormRef = ref();
const state = reactive({
	isShowDialog: false,
	ruleForm: {} as UpdateCodeGenInput | any,
	tableData: [] as any,
	tableData2: [] as any,
	tableData3: [] as any,
	dbData: [] as any,
	columnData: [] as any,
	lcolumnData: [] as any,
	bcolumnData: [] as any,
	menuData: [] as Array<SysMenu>,
	printList: [] as Array<SysPrint>,
});
const activeName = ref('1');
const activeTab = ref('codeGen');
const templateTableRef = ref();
const multipleSelection = ref([] as any);
const templateTableData = ref([] as any);
// 级联选择器配置选项
const cascaderProps = { checkStrictly: true, emitPath: false, value: 'id', label: 'title' };

onMounted(async () => {
	state.dbData = await getAPI(SysCodeGenApi)
		.apiSysCodeGenDatabaseListGet()
		.then((res) => res.data.result ?? []);

	state.printList = await getAPI(SysPrintApi)
		.apiSysPrintPagePost()
		.then((res) => res.data.result?.items ?? []);
	state.menuData = await getAPI(SysMenuApi)
		.apiSysMenuListGet()
		.then((res) => res.data.result ?? []);
});

// 获得模板列表
const getSysCodeGenTemplateList = async () => {
	let res = await getAPI(SysCodeGenTemplateApi).apiSysCodeGenTemplateListGet();
	let data = res.data.result ?? [];
	templateTableData.value = data;
	// 选中
	nextTick(() => {
		let checkedRows = [] as any;
		if (state.ruleForm.id) {
			// 修改
			data.forEach((element: any) => {
				if (state.ruleForm.codeGenTemplateRelations.some((ele: any) => ele.templateId == element.id)) {
					checkedRows.push(element);
					templateTableRef.value.toggleRowSelection(element, true);
				}
			});
		} else {
			// 新增
			data.forEach((element: any) => {
				if (element.isDefault) {
					checkedRows.push(element);
					templateTableRef.value.toggleRowSelection(element, true);
				}
			});
		}

		multipleSelection.value = checkedRows;
	});
};

// 表格选中事件
const handleSelectionChange = (val: any[]) => {
	multipleSelection.value = val;
	// console.log(val);
};

// db改变
const dbChanged = async () => {
	if (state.ruleForm.configId === '' || state.ruleForm.configId == null) return;
	state.tableData = await getAPI(SysCodeGenApi)
		.apiSysCodeGenTableListConfigIdGet(state.ruleForm.configId as string)
		.then((res) => res.data.result ?? []);

	let db = state.dbData.filter((u: any) => u.configId == state.ruleForm.configId);
	state.ruleForm.connectionString = db[0].connectionString;
	state.ruleForm.dbType = db[0].dbType.toString();
};
const dbChanged2 = async () => {
	if (state.ruleForm.configId === '' || state.ruleForm.configId == null) return;
	state.tableData2 = await getAPI(SysCodeGenApi)
		.apiSysCodeGenTableListConfigIdGet(state.ruleForm.configId2 as string)
		.then((res) => res.data.result ?? []);
};
const dbChanged3 = async () => {
	if (state.ruleForm.configId === '' || state.ruleForm.configId == null) return;
	state.tableData3 = await getAPI(SysCodeGenApi)
		.apiSysCodeGenTableListConfigIdGet(state.ruleForm.configId3 as string)
		.then((res) => res.data.result ?? []);
};
// table改变
const tableChanged = (item: any) => {
	state.ruleForm.tableName = item.entityName;
	state.ruleForm.busName = item.tableComment;
	state.ruleForm.tableUniqueList = [];
	getColumnInfoList(item);
};

const tabTypeChanged = async (item: any) => {
	state.ruleForm.tabType = item;
};
const treeNameChanged = (item: any) => {
	state.ruleForm.treeName = item.columnName;
};

const treeKeyChanged = (item: any) => {
	state.ruleForm.treeKey = item.columnName;
};
const leftTableChanged = (item: any) => {
	state.ruleForm.leftTab = item.entityName;
	console.log('leftTableChanged--', JSON.stringify(item));
	getLColumnInfoList(item);
};
const leftKeyChanged = (item: any) => {
	console.log('leftKeyChanged--', JSON.stringify(item));
	state.ruleForm.leftKey = item.columnName;
};
const leftPrimaryKeyChanged = (item: any) => {
	console.log('leftPrimaryKeyChanged--', JSON.stringify(item));
	state.ruleForm.leftPrimaryKey = item.columnName;
};
const leftNameChanged = (item: any) => {
	state.ruleForm.leftName = item.columnName;
};
const getLColumnInfoList = async (item: any) => {
	if (state.ruleForm.configId2 == '' || state.ruleForm.configId2 == null || state.ruleForm.leftTab == '' || state.ruleForm.leftTab == null) return;
	state.lcolumnData =
		(await getAPI(SysCodeGenApi)
			.apiSysCodeGenColumnListByTableNameTableNameConfigIdGet(item.tableName, state.ruleForm.configId2)
			.then((res) => res.data.result)) ?? [];
};
const bottomTableChanged = (item: any) => {
	state.ruleForm.bottomTab = item.entityName;
	getLBColumnInfoList(item);
};
const bottomKeyChanged = (item: any) => {
	state.ruleForm.bottomKey = item.columnName;
};
const bottomPrimaryKeyChanged = (item: any) => {
	console.log('bottomPrimaryKeyChanged--', JSON.stringify(item));
	state.ruleForm.bottomPrimaryKey = item.columnName;
};
const getLBColumnInfoList = async (item: any) => {
	if (state.ruleForm.configId3 == '' || state.ruleForm.configId3 == null || state.ruleForm.bottomTab == '' || state.ruleForm.bottomTab == null) return;
	state.bcolumnData =
		(await getAPI(SysCodeGenApi)
			.apiSysCodeGenColumnListByTableNameTableNameConfigIdGet(item.tableName, state.ruleForm.configId3)
			.then((res) => res.data.result)) ?? [];
};
// 表唯一约束配置项字段改变事件
const changeTableUniqueColumn = (value: any, index: number) => {
	if (value?.length === 1 && !state.ruleForm.tableUniqueList[index].message) {
		state.ruleForm.tableUniqueList[index].message = state.columnData.find((u: any) => u.columnName === value[0])?.columnComment;
	}
};

const getColumnInfoList = async (item: any) => {
	if (state.ruleForm.configId == '' || state.ruleForm.configId == null || state.ruleForm.tableName == '' || state.ruleForm.tableName == null) return;
	state.columnData =
		(await getAPI(SysCodeGenApi)
			.apiSysCodeGenColumnListByTableNameTableNameConfigIdGet(item.tableName, state.ruleForm.configId)
			.then((res) => res.data.result)) ?? [];
};

// 菜单改变
const menuChange = (menu: any) => {
	state.ruleForm.pagePath = state.menuData.find((x) => x.id == menu)?.name;
};

// print改变
const printTypeChanged = () => {
	if (state.ruleForm.printType === '') return;
	if (state.ruleForm.printType == 'off') state.ruleForm.printName = '';
};

// 获取全局组件大小
const getGlobalComponentSize = computed(() => {
	return other.globalComponentSize();
});

// 打开弹窗
const openDialog = (row: any) => {
	state.ruleForm = JSON.parse(JSON.stringify(row));
	dbChanged().then(() => getColumnInfoList(row));
	state.isShowDialog = true;
	ruleFormRef.value?.resetFields();
	getSysCodeGenTemplateList();
};

// 关闭弹窗
const closeDialog = () => {
	emits('handleQuery');
	state.isShowDialog = false;
	activeTab.value = 'codeGen';
};

// 取消
const cancel = () => {
	state.isShowDialog = false;
	activeTab.value = 'codeGen';
};

// 提交
const submit = () => {
	// 检查是否选中有模板
	if (multipleSelection.value.length == 0) {
		ElMessage({
			message: `请选择模板`,
			type: 'error',
		});
		activeTab.value = 'template';
		return;
	}

	let codeGenTemplateIds: any[] = [];
	multipleSelection.value.forEach((item: any) => {
		codeGenTemplateIds.push(item.id);
	});
	state.ruleForm.codeGenTemplateIds = codeGenTemplateIds;
	ruleFormRef.value.validate(async (valid: boolean) => {
		if (!valid) return;
		if (state.ruleForm.tableUniqueList?.length === 0) state.ruleForm.tableUniqueList = null;
		if (state.ruleForm.id != undefined && state.ruleForm.id > 0) {
			await getAPI(SysCodeGenApi).apiSysCodeGenUpdatePost(state.ruleForm as UpdateCodeGenInput);
		} else {
			await getAPI(SysCodeGenApi).apiSysCodeGenAddPost(state.ruleForm as AddCodeGenInput);
		}
		closeDialog();
	});
};

// 导出对象
defineExpose({ openDialog });
</script>

<style lang="scss" scoped>
:deep(.el-dialog__body) {
	min-height: 450px;
}
</style>
