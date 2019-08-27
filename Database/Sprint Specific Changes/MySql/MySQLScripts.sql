ALTER TABLE `tt_fav_reports` ADD `phase_id` INT(11) NULL DEFAULT NULL AFTER `project_id`;

ALTER TABLE `tt_fav_reports` ADD `show_phase` TINYINT(4) NOT NULL DEFAULT '0' AFTER `show_project`;