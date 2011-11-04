<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:MembershipRequest="fba:MembershipRequest">
  <xsl:template match="/">
    <html>
      <head>
        <title>审批进行中</title>
      </head>
      <body>
        <div>
          <br />
          <p>非常感谢你申请网站 <xsl:value-of select="MembershipRequest:get_SiteName()" /> 的会员。</p>

          <p>一旦你的申请获得通过，你将会收到用于首次登入的临时密码。</p>

          <p>如果你有其他问题或需要帮助，请联系管理员。</p>

        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>