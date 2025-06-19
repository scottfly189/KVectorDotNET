import { http } from 'uview-plus'

// 获取国密公钥私钥对
export const smKeyPairApi = (params) => http.get('/api/sysCommon/smKeyPair', params)

// 获取MD5加密字符串
export const mD5EncryptApi = (params) => http.get('/api/sysCommon/mD5Encrypt', params)

// 国密SM2加密字符串
export const sM2EncryptApi = (data) => http.post('/api/sysCommon/sM2Encrypt', data)

// 国密SM2解密字符串
export const sM2DecryptApi = (data) => http.post('/api/sysCommon/sM2Decrypt', data)

// 获取所有接口/动态API
export const apiListApi = (params) => http.get('/api/sysCommon/apiList', params)

// 获取所有移动端接口
export const appApiListApi = (params) => http.get('/api/sysCommon/appApiList', params)

// 生成所有移动端接口
export const generateAppApiApi = (params) => http.get('/api/sysCommon/generateAppApi', params)

// 下载标记错误的临时 Excel
export const downloadErrorExcelTempApi = (data) => http.post('/api/sysCommon/downloadErrorExcelTemp', data)

// 获取机器序列号
export const machineSerialKeyApi = (params) => http.get('/api/sysCommon/machineSerialKey', params)

// 性能压力测试
export const stressTestApi = (data) => http.post('/api/sysCommon/stressTest', data)

