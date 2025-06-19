import { http } from 'uview-plus'

// 获取字典值分页列表
export const pageApi = (data) => http.post('/api/sysDictData/page', data)

// 获取字典值列表
export const listApi = (params) => http.get('/api/sysDictData/list', params)

// 增加字典值
export const addApi = (data) => http.post('/api/sysDictData/add', data)

// 更新字典值
export const updateApi = (data) => http.post('/api/sysDictData/update', data)

// 删除字典值
export const deleteApi = (data) => http.post('/api/sysDictData/delete', data)

// 获取字典值详情
export const detailApi = (params) => http.get('/api/sysDictData/detail', params)

// 修改字典值状态
export const setStatusApi = (data) => http.post('/api/sysDictData/setStatus', data)

// 根据字典类型编码获取字典值集合
export const dataListApi = (params) => http.get('/api/sysDictData/dataList', params)

// 根据查询条件获取字典值集合
export const dataListApi = (params) => http.get('/api/sysDictData/dataList', params)

