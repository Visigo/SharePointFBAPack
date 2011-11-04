<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:MembershipRequest="fba:MembershipRequest">
  <xsl:template match="/">
    <html>
      <head>
        <title>申请拒绝</title>
      </head>
      <body>
        <div>
          <p>
            你申请网站 <xsl:value-of select="MembershipRequest:get_SiteName()" /> 的会员已经被拒绝。
          </p>

          <p>如你觉得这是个错误，请重新申请或联系网站管理员。</p>

          <p>谢谢！</p>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>