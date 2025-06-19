import { http } from 'uview-plus'

// 获取文件分页列表
export const pageApi = (data) => http.post('/api/sysFile/page', data)

// 上传文件Base64
export const uploadFileFromBase64Api = (data) => http.post('/api/sysFile/uploadFileFromBase64', data)

// 上传多文件
export const uploadFilesApi = (data) => http.post('/api/sysFile/uploadFiles', data)

// 根据文件Id或Url下载
export const downloadFileApi = (data) => http.post('/api/sysFile/downloadFile', data)

// 文件预览
export const previewApi = (params) => http.get('/api/sysFile/preview', params)

// 下载指定文件Base64格式
export const downloadFileBase64Api = (data) => http.post('/api/sysFile/downloadFileBase64', data)

// 删除文件
export const deleteApi = (data) => http.post('/api/sysFile/delete', data)

// 更新文件
export const updateApi = (data) => http.post('/api/sysFile/update', data)

// 获取文件
export const fileApi = (params) => http.get('/api/sysFile/file', params)

// 获取文件路径
export const folderApi = (params) => http.get('/api/sysFile/folder', params)

// 上传文件
export const uploadFileApi = (data) => http.post('/api/sysFile/uploadFile', data)

// 上传头像
export const uploadAvatarApi = (data) => http.post('/api/sysFile/uploadAvatar', data)

// 上传电子签名
export const uploadSignatureApi = (data) => http.post('/api/sysFile/uploadSignature', data)

// 根据关联查询附件
export const relationFilesApi = (params) => http.get('/api/sysFile/relationFiles', params)

