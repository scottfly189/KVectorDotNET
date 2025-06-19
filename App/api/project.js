import { http } from 'uview-plus'

// 获取项目列表
export const listApi = (params) => http.get('/api/goview/project/list', params)

// 新增项目
export const createApi = (data) => http.post('/api/goview/project/create', data)

// 修改项目
export const editApi = (data) => http.post('/api/goview/project/edit', data)

// 删除项目
export const deleteApi = (data) => http.delete('/api/goview/project/delete', data)

// 修改发布状态
export const publishApi = (data) => http.put('/api/goview/project/publish', data)

// 获取项目数据
export const getDataApi = (params) => http.get('/api/goview/project/getData', params)

// 保存项目数据
export const save/dataApi = (data) => http.post('/api/goview/project/save/data', data)

// 上传预览图
export const uploadApi = (data) => http.post('/api/goview/project/upload', data)

// 获取预览图
export const getIndexImageApi = (params) => http.get('/api/goview/project/getIndexImage', params)

// 上传背景图
export const uploadBackGroundApi = (data) => http.post('/api/goview/project/uploadBackGround', data)

// 获取背景图
export const getBackGroundImageApi = (params) => http.get('/api/goview/project/getBackGroundImage', params)

