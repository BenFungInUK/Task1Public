module.exports = {
    devServer: {
      proxy: {
        '^/api/': {
          target: 'https://localhost:5001',
          pathRewrite: { "^/api/": "/api/" },
          changeOrigin: true,
          logLevel: "debug"
        },
      }
    }
  }
