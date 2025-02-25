CREATE PROCEDURE `GetCampains`(
    IN filterArg VARCHAR(500),
    IN sortArg VARCHAR(500),
    IN numberOfRecordArg INT,
    IN offsetArg INT
    )
BEGIN
    SET @filter = IF(filterArg IS NULL OR filterArg = '', ' c.IsDeleted = 0 ', filterArg);
    SET @sort = IF(sortArg IS NULL OR sortArg = '', ' c.CreatedDate DESC ', sortArg);
    SET @selectStatement = CONCAT('
        SELECT
            c.CampaignId, c.Title, c.Content, c.Status, c.MetadataId, m.Name MetadataName, m.S3Url MetadataS3Url, m.Hyperlink, m.OrderNumber, mc.Name MetadataCategoryName, c.CreatedDate
        FROM MagicTree.Campaign c
        JOIN MagicTree.Metadata m USING(MetadataId)
        JOIN MagicTree.MetadataCategory mc USING (MetadataCategoryId)
        WHERE ', @filter, '
        ORDER BY ', @sort, '
        LIMIT ', numberOfRecordArg, ' 
        OFFSET ', offsetArg, ';');

    SET @countStatement = CONCAT('
        SELECT COUNT(*) AS TotalRecord
        FROM MagicTree.Campaign c
        JOIN MagicTree.Metadata m USING(MetadataId)
        JOIN MagicTree.MetadataCategory mc USING (MetadataCategoryId)
        WHERE ', @filter, ';');

    PREPARE selectStatement FROM @selectStatement;
    EXECUTE selectStatement;
    DEALLOCATE PREPARE selectStatement;

    PREPARE countStatement FROM @countStatement;
    EXECUTE countStatement;
    DEALLOCATE PREPARE countStatement;
END