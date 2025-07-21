FROM node:20.11-slim

# 安装必要的依赖和构建工具
RUN apt-get update && apt-get install -y \
    bash

# 设置工作目录
WORKDIR /app

# 首先只复制依赖相关文件以利用缓存
COPY package*.json ./

# 安装依赖
RUN npm install

# 复制所有文件
COPY . .

# 构建应用
RUN npm run build

# 设置环境变量
ENV NITRO_PRESET=node-server \
    HOST=0.0.0.0 \
    PORT=3000

# 暴露端口
EXPOSE 3000

# 启动应用
CMD ["node", ".output/server/index.mjs"]
