import { http } from 'uview-plus'

// 获取代码生成分页列表
export const pageApi = (data) => http.post('/api/sysCodeGen/page', data)

// 增加代码生成
export const addApi = (data) => http.post('/api/sysCodeGen/add', data)

// 更新代码生成
export const updateApi = (data) => http.post('/api/sysCodeGen/update', data)

// 删除代码生成
export const deleteApi = (data) => http.post('/api/sysCodeGen/delete', data)

// 获取代码生成详情
export const detailApi = (params) => http.get('/api/sysCodeGen/detail', params)

// 获取数据库库集合
export const databaseListApi = (params) => http.get('/api/sysCodeGen/databaseList', params)

// 获取数据库表(实体)集合
export const tableListApi = (params) => http.get('/api/sysCodeGen/tableList', params)

// 根据表名获取列集合
export const columnListByTableNameApi = (params) => http.get('/api/sysCodeGen/columnListByTableName', params)

// 获取程序保存位置
export const applicationNamespacesApi = (params) => http.get('/api/sysCodeGen/applicationNamespaces', params)

// 执行代码生成
export const runLocalApi = (data) => http.post('/api/sysCodeGen/runLocal', data)

// 获取代码生成预览
export const previewApi = (data) => http.post('/api/sysCodeGen/preview', data)

