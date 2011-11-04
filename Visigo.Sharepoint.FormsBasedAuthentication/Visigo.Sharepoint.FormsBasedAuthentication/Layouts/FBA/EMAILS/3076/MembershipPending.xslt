<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:MembershipRequest="fba:MembershipRequest">
  <xsl:template match="/">
    <html>
      <head>
        <title>審批進行中</title>
      </head>
      <body>
        <div>
          <br />
          <p>
            非常感謝你申請網站 <xsl:value-of select="MembershipRequest:get_SiteName()" /> 的會員。
          </p>

          <p>一旦你的申請獲得通過，你將會收到用於首次登入的臨時密碼。</p>

          <p>如果你有其他問題或需要幫助，請聯繫管理員。</p>

        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>