import { http } from 'uview-plus'

// 获取库列表
export const listApi = (params) => http.get('/api/sysDatabase/list', params)

// 获取可视化库表结构
export const visualDbTableApi = (params) => http.get('/api/sysDatabase/visualDbTable', params)

// 获取字段列表
export const columnListApi = (params) => http.get('/api/sysDatabase/columnList', params)

// 获取数据库数据类型列表
export const dbTypeListApi = (params) => http.get('/api/sysDatabase/dbTypeList', params)

// 增加列
export const addColumnApi = (data) => http.post('/api/sysDatabase/addColumn', data)

// 删除列
export const deleteColumnApi = (data) => http.post('/api/sysDatabase/deleteColumn', data)

// 编辑列
export const updateColumnApi = (data) => http.post('/api/sysDatabase/updateColumn', data)

// 获取表列表
export const tableListApi = (params) => http.get('/api/sysDatabase/tableList', params)

// 增加表
export const addTableApi = (data) => http.post('/api/sysDatabase/addTable', data)

// 删除表
export const deleteTableApi = (data) => http.post('/api/sysDatabase/deleteTable', data)

// 编辑表
export const updateTableApi = (data) => http.post('/api/sysDatabase/updateTable', data)

// 创建实体
export const createEntityApi = (data) => http.post('/api/sysDatabase/createEntity', data)

// 创建种子数据
export const createSeedDataApi = (data) => http.post('/api/sysDatabase/createSeedData', data)

// 备份数据库（PostgreSQL）
export const backupDatabaseApi = (data) => http.post('/api/sysDatabase/backupDatabase', data)

