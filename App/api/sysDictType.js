import { http } from 'uview-plus'

// 获取字典类型分页列表
export const pageApi = (data) => http.post('/api/sysDictType/page', data)

// 获取字典类型列表
export const listApi = (params) => http.get('/api/sysDictType/list', params)

// 获取字典类型-值列表
export const dataListApi = (params) => http.get('/api/sysDictType/dataList', params)

// 添加字典类型
export const addApi = (data) => http.post('/api/sysDictType/add', data)

// 更新字典类型
export const updateApi = (data) => http.post('/api/sysDictType/update', data)

// 删除字典类型
export const deleteApi = (data) => http.post('/api/sysDictType/delete', data)

// 获取字典类型详情
export const detailApi = (params) => http.get('/api/sysDictType/detail', params)

// 修改字典类型状态
export const setStatusApi = (data) => http.post('/api/sysDictType/setStatus', data)

// 获取所有字典集合
export const allDictListApi = (params) => http.get('/api/sysDictType/allDictList', params)

