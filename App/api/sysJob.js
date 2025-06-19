import { http } from 'uview-plus'

// 获取作业分页列表
export const pageJobDetailApi = (data) => http.post('/api/sysJob/pageJobDetail', data)

// 获取作业组名称集合
export const listJobGroupApi = (data) => http.post('/api/sysJob/listJobGroup', data)

// 添加作业
export const addJobDetailApi = (data) => http.post('/api/sysJob/addJobDetail', data)

// 更新作业
export const updateJobDetailApi = (data) => http.post('/api/sysJob/updateJobDetail', data)

// 删除作业
export const deleteJobDetailApi = (data) => http.post('/api/sysJob/deleteJobDetail', data)

// 获取触发器列表
export const jobTriggerListApi = (params) => http.get('/api/sysJob/jobTriggerList', params)

// 添加触发器
export const addJobTriggerApi = (data) => http.post('/api/sysJob/addJobTrigger', data)

// 更新触发器
export const updateJobTriggerApi = (data) => http.post('/api/sysJob/updateJobTrigger', data)

// 删除触发器
export const deleteJobTriggerApi = (data) => http.post('/api/sysJob/deleteJobTrigger', data)

// 暂停所有作业
export const pauseAllJobApi = (data) => http.post('/api/sysJob/pauseAllJob', data)

// 启动所有作业
export const startAllJobApi = (data) => http.post('/api/sysJob/startAllJob', data)

// 暂停作业
export const pauseJobApi = (data) => http.post('/api/sysJob/pauseJob', data)

// 启动作业
export const startJobApi = (data) => http.post('/api/sysJob/startJob', data)

// 取消作业
export const cancelJobApi = (data) => http.post('/api/sysJob/cancelJob', data)

// 执行作业
export const runJobApi = (data) => http.post('/api/sysJob/runJob', data)

// 暂停触发器
export const pauseTriggerApi = (data) => http.post('/api/sysJob/pauseTrigger', data)

// 启动触发器
export const startTriggerApi = (data) => http.post('/api/sysJob/startTrigger', data)

// 强制唤醒作业调度器
export const cancelSleepApi = (data) => http.post('/api/sysJob/cancelSleep', data)

// 强制触发所有作业持久化
export const persistAllApi = (data) => http.post('/api/sysJob/persistAll', data)

// 获取集群列表
export const jobClusterListApi = (params) => http.get('/api/sysJob/jobClusterList', params)

// 获取作业触发器运行记录分页列表
export const pageJobTriggerRecordApi = (data) => http.post('/api/sysJob/pageJobTriggerRecord', data)

// 清空作业触发器运行记录
export const clearJobTriggerRecordApi = (data) => http.post('/api/sysJob/clearJobTriggerRecord', data)

