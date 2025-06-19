import { http } from 'uview-plus'

// 生成签名
export const generateSignatureApi = (data) => http.post('/api/sysOpenAccess/generateSignature', data)

// 获取开放接口身份分页列表
export const pageApi = (data) => http.post('/api/sysOpenAccess/page', data)

// 增加开放接口身份
export const addApi = (data) => http.post('/api/sysOpenAccess/add', data)

// 更新开放接口身份
export const updateApi = (data) => http.post('/api/sysOpenAccess/update', data)

// 删除开放接口身份
export const deleteApi = (data) => http.post('/api/sysOpenAccess/delete', data)

// 创建密钥
export const secretApi = (data) => http.post('/api/sysOpenAccess/secret', data)

